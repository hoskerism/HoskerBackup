using HoskerBackup.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoskerBackup
{
	class Config
	{
		public Config()
		{
			IncludeFolders = Helper.ConvertToSet(Settings.Default["IncludeFolders"].ToString().Split(','));
			ExcludeFolders = Helper.ConvertToSet(Settings.Default["ExcludeFolders"].ToString().Split(','));
			ExcludePatterns = Helper.ConvertToSet(Settings.Default["ExcludePatterns"].ToString().Split(','));
			ExcludePatterns.UnionWith(MandatoryExcludePatterns);

			Destination = Settings.Default["Destination"].ToString();
			ScheduleHour = (int)Settings.Default["ScheduleHour"];
			ScheduleMinute = (int)Settings.Default["ScheduleMinute"];
			KeepDeletedFilesFor = (int)Settings.Default["KeepDeletedFilesFor"];
		}

		public void SaveConfig()
		{
			Settings.Default["IncludeFolders"] = string.Join(",", IncludeFolders);
			Settings.Default["ExcludeFolders"] = string.Join(",", ExcludeFolders);
			
			ExcludePatterns.UnionWith(MandatoryExcludePatterns);
			Settings.Default["ExcludePatterns"] = string.Join(",", ExcludePatterns);

			Settings.Default["Destination"] = Destination;
			Settings.Default["ScheduleHour"] = ScheduleHour;
			Settings.Default["ScheduleMinute"] = ScheduleMinute;
			Settings.Default["KeepDeletedFilesFor"] = KeepDeletedFilesFor;
			
			Settings.Default.Save();
		}

		string[] MandatoryExcludePatterns
		{
			get
			{
				return new[]
				{
					"thumbs*.db",
					"*.tmp",
					"*.temp",
					"~$*.*",
					"~.backup",
					".DS_Store",
					"$RECYCLE.BIN",
					"Recycle Bin",
					"System Volume Information"
				};
			}
		}

		public SortedSet<string> IncludeFolders { get; set; }

		public SortedSet<string> ExcludeFolders { get; set; }

		public SortedSet<string> ExcludePatterns { get; set; }

		public string Destination { get; set; }

		public int ScheduleHour { get; set; }

		public int ScheduleMinute { get; set; }

		public int KeepDeletedFilesFor { get; set; }

		public bool IsValid
		{
			get 
			{
				var isValid = IncludeFolders.Any()					
					&& ScheduleHour >= 0 && ScheduleHour < 24
					&& ScheduleMinute >= 0 && ScheduleMinute < 60
					&& KeepDeletedFilesFor > 0;

				if (!isValid)
				{
					return false;
				}

				if (!Directory.Exists(Destination))
				{
					// Wait a few seconds then try again
					if (!iHaveWaited)
					{
						Thread.Sleep(10000);
						iHaveWaited = true;
					}

					return Directory.Exists(Destination);
				}

				return true;
			}
		}

		bool iHaveWaited = false;
	}
}
