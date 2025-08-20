using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HoskerBackup
{
	public class Backup
	{
		public Backup(Config config)
		{
			this.config = config;
		}

		public void RunBackup()
		{
			if (IsRunning)
			{
				return;
			}

			IsRunning = true;

			try
			{
				log.Clear();
				startTime = DateTime.Now;
				fileCount = 0;
				folderCount = 0;

				config = null;
				if (!Config.IsValid)
				{
					LogError("Config is invalid");
					Status = false;
					StatusMessage = "Config is invalid";

					throw new ApplicationException("Config is invalid");
				}

				Status = true;
				StatusMessage = "Running Backup at " + DateTime.Now.ToString("s");
				log.AppendLine(StatusMessage);

				BackupFolders();

				var timeTaken = DateTime.Now - startTime;

				StatusMessage = "Backed up " + fileCount + " file(s) in " + folderCount + " folder(s) in " + timeTaken.ToString("hh") + ":" + timeTaken.ToString("mm") + ":" + timeTaken.ToString("ss");
				log.AppendLine(StatusMessage);
			}
			catch (Exception e)
			{
				Status = false;
				StatusMessage = e.Message;
				ProgressMessage = e.Message;
				log.AppendLine(e.Message + "(" + e.GetType().ToString() + " at " + DateTime.Now.ToString("s") + ")");
				log.AppendLine(e.StackTrace);
			}
			finally
			{
				lastRun = DateTime.Now;
				IsRunning = false;
			}
		}

		public void Cancel()
		{
			cancel = true;
		}

		void BackupFolders()
		{
			foreach (var backupFolder in Config.IncludeFolders)
			{
				BackupFolder(backupFolder);
				if (cancel)
				{
					return;
				}
			}
		}

		void BackupFolder(string folder)
		{
			try
			{
				if (!Directory.Exists(folder))
				{
					LogWarning("Folder does not exist: " + folder);
					Status = false;
				}

				if (ShouldBackupFolder(folder))
				{
					folderCount++;
					ProgressMessage = "Backing up folder: " + folder;

					var folderLogs = ReadLog(folder);

					try
					{
						foreach (var file in new DirectoryInfo(folder).GetFiles())
						{
							BackupFile(folder, file.Name, folderLogs);
							if (cancel)
							{
								return;
							}
						}

						foreach (var subFolder in Directory.GetDirectories(folder))
						{
							BackupFolder(subFolder);
							if (cancel)
							{
								return;
							}
						}
					}
					catch (UnauthorizedAccessException)
					{
						LogWarning("Access denied to folder: " + folder);
					}

					HandleDeletedFiles(folder, folderLogs);

					WriteLog(folder, folderLogs);
				}
			}
			catch (Exception e)
			{
				Status = false;
				LogError("Backing up folder: " + folder);
				log.AppendLine(e.Message + "(" + e.GetType().ToString() + " at " + DateTime.Now.ToString("s") + ")");
				log.AppendLine(e.StackTrace);

				if (IsCriticalException(e))
				{
					throw;
				}
			}
		}

		bool ShouldBackupFolder(string folder)
		{
			var folderName = Path.GetFileName(folder);
			var path = Path.GetDirectoryName(folder);
			if (MatchesExcludePattern(path, folderName))
			{
				return false;
			}

			var attributes = File.GetAttributes(folder);
			if ((attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
			{
				return false;
			}
			else if ((attributes & FileAttributes.System) == FileAttributes.System)
			{
				LogWarning("Skipping system folder: " + folder);
				return false;
			}

			return !Config.ExcludeFolders.Contains(folder);
		}

		void BackupFile(string folder, string filename, List<string> folderLogs)
		{
			var debugLog = new StringBuilder();

			var destinationFolder = "";
			var destinationFilename = "";

			try
			{
				if (ShouldBackupFile(folder, filename, folderLogs))
				{
					// TODO: use this instead: Path.Combine(folder, filename);
					// Note: there is also Path.GetFilename
					ProgressMessage = "Backing up: " + Path.Combine(folder, filename);
					debugLog.AppendLine(ProgressMessage);

					destinationFolder = GetDestinationFolderName(folder);
					debugLog.AppendLine("Destination Folder: " + folder);

					destinationFilename = GetDestinationFilename(destinationFolder, filename, folderLogs);


					CopyFile(folder, filename, destinationFolder, destinationFilename);

					fileCount++;

					StatusMessage = "Backup in progress - " + fileCount + " file(s) in " + folderCount + " folder(s)";
				}
			}
			catch (Exception e)
			{
				Status = false;
				LogError("Backing up file: " + folder + "\\" + filename + " with max FileLength " + Helper.GetMaxFilenameLength(destinationFolder));
				log.AppendLine(e.Message + "(" + e.GetType().ToString() + ")");
				log.AppendLine(e.StackTrace);

				if (IsCriticalException(e))
				{
					throw;
				}
			}
		}

		void CopyFile(string folder, string filename, string destinationFolder, string destinationFilename)
		{
			var debugLog = new StringBuilder();

			try
			{
				debugLog.AppendLine("CopyFile(" + folder + ", " + filename + ", " + destinationFolder + ", " + destinationFilename + ")");

				CreateDirectory(destinationFolder);
				debugLog.AppendLine("Created folder: " + destinationFolder);

				var overwrite = !VersionFile(destinationFolder, destinationFilename);
				debugLog.AppendLine("Overwrite: " + overwrite);
				File.Copy(Path.Combine(folder, filename), Path.Combine(destinationFolder, destinationFilename), overwrite);
			}
			catch (Exception e)
			{
				Status = false;
				LogError(e.Message + "\r\n" + debugLog.ToString());
			}
		}

		void CreateDirectory(string destinationFolder)
		{
			if (!Directory.Exists(destinationFolder))
			{
				Directory.CreateDirectory(destinationFolder);
			}
		}

		bool VersionFile(string destinationFolder, string destinationFilename)
		{
			if (File.Exists(destinationFolder + "\\" + destinationFilename))
			{
				for (int i = maxVersions - 1; i >= 0; i--)
				{
					var filenameB = GetVersionFilename(destinationFilename, i);
					var filenameA = GetVersionFilename(destinationFilename, i - 1);

					if (destinationFolder.Length + filenameB.Length + 1 > maxFilePath)
					{
						LogWarning("Unable to version file: " + Path.Combine(destinationFolder, destinationFilename) + ". File path is too long.");
						return false;
					}

					if (File.Exists(Path.Combine(destinationFolder, filenameB)))
					{
						File.Delete(Path.Combine(destinationFolder, filenameB));
					}

					if (File.Exists(Path.Combine(destinationFolder, filenameA)))
					{
						File.Move(Path.Combine(destinationFolder, filenameA), Path.Combine(destinationFolder, filenameB));
					}
				}
			}

			return true;
		}

		string GetVersionFilename(string filename, int version)
		{
			if (version < 0)
			{
				return filename;
			}

			if (filename.Contains("."))
			{
				(var stem, var extension) = SplitFilename(filename);
				return stem + "_" + version + "." + extension;
			}
			else
			{
				return filename + "_" + version;
			}
		}

		bool ShouldBackupFile(string folder, string filename, List<string> folderLogs)
		{
			if (MatchesExcludePattern(folder, filename, folderLogs))
			{
				return false;
			}

			var destinationFolder = GetDestinationFolderName(folder);
			var destinationFilename = GetDestinationFilename(destinationFolder, filename, folderLogs);
			if (!File.Exists(destinationFolder + "\\" + destinationFilename))
			{
				return true;
			}

			var sourceFileInfo = new FileInfo(folder + "\\" + filename);
			var destinationFileInfo = new FileInfo(destinationFolder + "\\" + destinationFilename);

			return sourceFileInfo.LastWriteTime != destinationFileInfo.LastWriteTime
				|| sourceFileInfo.Length != destinationFileInfo.Length;
		}

		bool MatchesExcludePattern(string folder, string name, List<string> folderLogs = null)
		{
			foreach (var excludePattern in Config.ExcludePatterns)
			{
				if (Microsoft.VisualBasic.CompilerServices.LikeOperator.LikeString(name, excludePattern, Microsoft.VisualBasic.CompareMethod.Text))
				{
					if (folderLogs != null)
					{
						AddFolderLog("File / Folder excluded", folder + "\\" + name, "Excluded due to pattern: " + excludePattern, folderLogs);
					}

					return true;
				}
			}

			return false;
		}

		string GetDestinationFilename(string destinationFolder, string filename, List<string> folderLogs)
		{
			return ShortenFilename(destinationFolder, filename, folderLogs);
		}

		string GetDestinationFolderName(string folder)
		{
			string destinationFolderName = Config.DestinationDirectory + "\\" + folder
				.Replace(":", "")
				.Replace("\\\\", "\\");

			if (destinationFolderName.Length > maxFolderPath)
			{
				throw new ApplicationException("Folder path exceed max allowable: " + destinationFolderName);
			}

			return destinationFolderName;
		}

		string ShortenFilename(string destinationFolder, string filename, List<string> folderLogs)
		{
			if (Path.Combine(destinationFolder, filename).Length <= maxFilePath && filename.Length < MaxFilenameLength)
			{
				return filename;
			}

			var shortenedFiles = new Dictionary<string, string>();
			foreach (var log in folderLogs.Where(l => l.StartsWith("Filename Truncated: ")).ToArray())
			{
				var shortenedFile = log.Substring("Filename Truncated: ".Length).Split('|');
				if (shortenedFile[1].Length <= MaxFilenameLength)
				{
					shortenedFiles.Add(shortenedFile[0], shortenedFile[1]);
				}
			}

			if (shortenedFiles.ContainsKey(filename))
			{
				return shortenedFiles[filename];
			}

			var shrinkFilenameBy = 0;
			if (filename.Length > MaxFilenameLength)
			{
				shrinkFilenameBy = filename.Length - MaxFilenameLength;
			}

			var shrinkPathBy = destinationFolder.Length + filename.Length - shrinkFilenameBy + 1 - maxFilePath;

			var shrinkBy = Math.Max(shrinkFilenameBy, shrinkPathBy);

			for (var i = 0; i <= 1000; i++)
			{
				if (i == 1000)
				{
					throw new ApplicationException("No available index for shortened filename: " + destinationFolder + filename);
				}

				var index = i.ToString();

				(var stem, var extension) = SplitFilename(filename);

				if (stem.Length < shrinkBy + index.Length + 1)
				{
					throw new ApplicationException("Can't shorten filename " + filename + " by " + shrinkBy + index.Length + 1);
				}

				var shortenedFilename = stem.Substring(0, filename.Length - extension.Length - shrinkBy - index.Length - 2) + "~" + index;
				if (extension != "")
				{
					shortenedFilename += "." + extension;
				}

				if (!shortenedFiles.ContainsValue(shortenedFilename))
				{
					AddFolderLog("Filename Truncated", filename, shortenedFilename, folderLogs);
					log.AppendLine("Filename Truncated in " + destinationFolder + ": " + filename + ": " + shortenedFilename);

					if (shortenedFilename.Length > MaxFilenameLength)
					{
						LogError("Filename truncated but still exceeds max length");
					}

					return shortenedFilename;
				}
			}

			throw new ApplicationException("Couldn't shorten filename for some reason: " + destinationFolder + filename);
		}

		(string, string) SplitFilename(string filename)
		{
			var allParts = filename.Split('.');

			var stem = filename;
			var extension = "";

			if (allParts.Length > 1)
			{
				extension = allParts[allParts.Length - 1];
				stem = filename.Substring(0, filename.Length - extension.Length - 1);
			}

			return (stem, extension);
		}

		void HandleDeletedFiles(string folder, List<string> folderLogs)
		{
			var destinationFolder = GetDestinationFolderName(folder);
			if (Directory.Exists(destinationFolder))
			{
				LogDeletedFiles(folder, folderLogs);
				DeleteOldBackupFiles(destinationFolder, folderLogs);

				LogDeletedFolders(folder, folderLogs);
				DeleteOldBackupFolders(destinationFolder, folderLogs);
			}
		}

		void LogDeletedFiles(string folder, List<string> folderLogs)
		{
			var destinationFolder = GetDestinationFolderName(folder);

			foreach (var destinationFile in new DirectoryInfo(destinationFolder).GetFiles().Where(f => f.Name != logFileName))
			{
				if (IsVersionFile(destinationFolder, destinationFile.Name) || MatchesExcludePattern(destinationFolder, destinationFile.Name))
				{
					continue;
				}

				var originalFilename = destinationFile.Name;
				var shortenedFiles = new Dictionary<string, string>();
				foreach (var log in folderLogs.Where(l => l.StartsWith("Filename Truncated: ")))
				{
					var shortenedFile = log.Substring("Filename Truncated: ".Length).Split('|');
					shortenedFiles.Add(shortenedFile[1], shortenedFile[0]);
				}

				if (shortenedFiles.ContainsKey(destinationFile.Name))
				{
					originalFilename = shortenedFiles[destinationFile.Name];
				}

				var deletedFileLogIndex = folderLogs.FindIndex(l => l.StartsWith("File Deleted: " + originalFilename + "|"));
				if (deletedFileLogIndex >= 0)
				{
					// File was previously deleted
					if (File.Exists(folder + "\\" + originalFilename))
					{
						// File has been recreated. Delete the log.
						folderLogs.RemoveAt(deletedFileLogIndex);
					}
				}
				else
				{
					if (!File.Exists(folder + "\\" + originalFilename))
					{
						AddFolderLog("File Deleted", originalFilename, DateTime.Now.ToString("s"), folderLogs);
					}
				}
			}
		}

		bool IsVersionFile(string folder, string filename)
		{
			var match = Regex.Match(filename, "_\\d{1,2}\\.");
			if (match.Success)
			{
				var originalFilename = filename.Replace(match.Value, ".");
				return File.Exists(folder + "\\" + originalFilename);
			}

			return false;
		}

		void DeleteOldBackupFiles(string destinationFolder, List<string> folderLogs)
		{
			foreach (var log in folderLogs.Where(l => l.StartsWith("File Deleted: ")).ToArray())
			{
				var logParams = log.Substring("File Deleted: ".Length).Split('|');
				var originalFilename = logParams[0];
				var dateDeleted = DateTime.Parse(logParams[1]);
				if ((DateTime.Now - dateDeleted).TotalDays > Config.KeepDeletedFilesFor)
				{
					for (var i = maxVersions - 1; i >= 0; i--)
					{
						// Note that versioning is not supported where originalFilename != destinationFilename, so we only need to check versions for originalFilename.
						var versionFilename = GetVersionFilename(originalFilename, i);
						if (File.Exists(destinationFolder + "\\" + versionFilename))
						{
							File.Delete(destinationFolder + "\\" + versionFilename);
						}
					}

					var destinationFilename = GetDestinationFilename(destinationFolder, originalFilename, folderLogs);
					if (File.Exists(destinationFolder + "\\" + destinationFilename))
					{
						File.Delete(destinationFolder + "\\" + destinationFilename);
						var logMessage = "Backup deleted on " + DateTime.Now.ToString("s") + " after " + Config.KeepDeletedFilesFor + " day(s)";
						AddFolderLog("Backup Deleted: ", originalFilename, logMessage, folderLogs);
						this.log.AppendLine("Backup Deleted: " + originalFilename + " - " + logMessage);

					}

					folderLogs.RemoveAll(l => l == log);
				}
			}
		}

		void LogDeletedFolders(string folder, List<string> folderLogs)
		{
			var destinationFolder = GetDestinationFolderName(folder);

			foreach (var backupFolder in new DirectoryInfo(destinationFolder).GetDirectories())
			{
				var deletedFolderLogIndex = folderLogs.FindIndex(l => l.StartsWith("Folder Deleted: " + backupFolder.Name + "|"));
				if (deletedFolderLogIndex >= 0)
				{
					// Folder was previously deleted
					if (Directory.Exists(Path.Combine(folder, backupFolder.Name)))
					{
						// Folder has been recreated. Delete the log.
						folderLogs.RemoveAt(deletedFolderLogIndex);
					}
				}
				else
				{
					if (!Directory.Exists(Path.Combine(folder, backupFolder.Name)))
					{
						AddFolderLog("Folder Deleted", backupFolder.Name, DateTime.Now.ToString("s"), folderLogs);
					}
				}
			}
		}

		void DeleteOldBackupFolders(string destinationFolder, List<string> folderLogs)
		{
			foreach (var log in folderLogs.Where(l => l.StartsWith("Folder Deleted: ")).ToArray())
			{
				var logParams = log.Substring("Folder Deleted: ".Length).Split('|');
				var backupFolderName = logParams[0];
				var dateDeleted = DateTime.Parse(logParams[1]);
				if ((DateTime.Now - dateDeleted).TotalDays > Config.KeepDeletedFilesFor)
				{
					if (Directory.Exists(Path.Combine(destinationFolder, backupFolderName)))
					{
						Directory.Delete(Path.Combine(destinationFolder, backupFolderName), true);
						var logMessage = "Folder backup deleted on " + DateTime.Now.ToString("s") + " after " + Config.KeepDeletedFilesFor + " day(s)";
						AddFolderLog("Backup Deleted: ", backupFolderName, logMessage, folderLogs);
						this.log.AppendLine("Backup Deleted: " + backupFolderName + " - " + logMessage);
					}

					folderLogs.RemoveAll(l => l == log);
				}
			}
		}

		void AddFolderLog(string logType, string fileReference, string message, List<string> folderLogs)
		{
			var log = logType + ": " + fileReference + "|" + message;

			var replaceLog = false;

			switch (logType)
			{
				case "File Excluded":
				case "Filename Truncated":
				case "Backup Deleted":
					replaceLog = true;
					break;

				case "File Deleted":
				case "Folder Deleted":
					replaceLog = false;
					break;

				default:
					replaceLog = false;
					break;
			}

			var index = folderLogs.FindIndex(l => l.StartsWith(logType + ": " + fileReference + "|"));
			if (index == -1)
			{
				folderLogs.Add(log);
			}
			else if (replaceLog)
			{
				folderLogs[index] = log;
			}
		}

		void LogError(string error)
		{
			log.AppendLine(DateTime.Now.ToShortTimeString() + " Error: " + error);
		}

		void LogWarning(string warning)
		{
			log.AppendLine(DateTime.Now.ToShortTimeString() + " Warning: " + warning);
		}

		List<string> ReadLog(string folder)
		{
			var logFile = GetDestinationFolderName(folder) + "\\" + logFileName;
			if (File.Exists(logFile))
			{
				var lines = File.ReadAllLines(logFile).ToList();
				return lines;
			}

			return new List<string>();
		}

		void WriteLog(string folder, List<string> logs)
		{
			var logsToWrite = logs.Where(l => l != "");
			if (logsToWrite.Any())
			{
				var destinationFolderName = GetDestinationFolderName(folder);
				CreateDirectory(destinationFolderName);
				File.WriteAllLines(destinationFolderName + "\\" + logFileName, logsToWrite);
			}
		}

		bool IsCriticalException(Exception e)
		{
			if (e.Message.Contains("There is not enough space on the disk."))
			{
				return true;
			}

			return false;
		}

		const string logFileName = "~.backup";

		public bool IsRunning { get; private set; }

		public bool Status { get; private set; } = true;

		public string ProgressMessage { get; private set; }

		public string StatusMessage { get; private set; } = "";

		public bool HasRunToday => lastRun.Date == DateTime.Today;

		DateTime lastRun;

		Config Config => config ?? (config = new Config());
		Config config;

		public string Log => log.ToString();
		StringBuilder log = new StringBuilder();

		DateTime startTime;

		long fileCount = 0;
		long folderCount = 0;

		const int maxFolderPath = 248;
		const int maxFilePath = 258;
		const int maxVersions = 10;

		int MaxFilenameLength
		{
			get
			{
				if (maxFilenameLength == 0)
				{
					maxFilenameLength = Helper.GetMaxFilenameLength(Config.DestinationDirectory);
				}

				return maxFilenameLength > 0 ? maxFilenameLength : maxFilePath;
			}
		}
		int maxFilenameLength;

		bool cancel;
	}
}
