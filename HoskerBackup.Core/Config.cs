using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HoskerBackup.Core
{
	public class Config
	{
		// Static mandatory exclusions (added back; not serialized to JSON)
		public static readonly List<string> MandatoryExclusionPatterns = new List<string>
	{
		"thumbs*.db",
		"*.tmp",
		"*.temp",
		"~$*.*",
		"~.backup",
		".DS_Store",
		"$RECYCLE.BIN",
		"Recycle Bin",
		"System Volume Information",
		"*.vsidx"
	};

		// Instance properties (as before)
		public List<string> IncludeFolders { get; set; } = new List<string>();
		public List<string> ExcludeFolders { get; set; } = new List<string>();
		public List<string> UserExcludePatterns { get; set; } = new List<string>();
		public string DestinationDirectory { get; set; } = "";
		public int ScheduleHour { get; set; } = 3; // Default to 3 AM
		public int ScheduleMinute { get; set; } = 0;
		public int KeepDeletedFilesFor { get; set; } = 7;
		public DateTime? LastRun { get; set; }

		public List<string> ExcludePatterns => UserExcludePatterns.Union(MandatoryExclusionPatterns).ToList<string>();

		public Config() { }

		// Validation method
		public bool IsValid
		{
			get
			{
				if (string.IsNullOrEmpty(DestinationDirectory) || !Directory.Exists(DestinationDirectory))
				{
					return false;
				}
				return true;
			}
		}

		// Save to JSON (static list not included)
		public void Save()
		{
			try
			{
				string configDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HoskerBackup");
				Directory.CreateDirectory(configDir);
				string configPath = Path.Combine(configDir, "config.json");
				string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(configPath, json);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error saving config: {ex.Message}");
			}
		}

		// Load from JSON
		public static Config Load()
		{
			try
			{
				string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HoskerBackup", "config.json");
				if (File.Exists(configPath))
				{
					string json = File.ReadAllText(configPath);
					return JsonSerializer.Deserialize<Config>(json);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error loading config: {ex.Message}");
			}
			return new Config();
		}
	}
}