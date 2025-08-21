using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HoskerBackup.Core; // Adjust to your Core namespace

namespace HoskerBackupService
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private List<Config> userConfigs = new List<Config>();

		public Worker(ILogger<Worker> logger)
		{
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			// Load configs from all non-system user profiles
			string usersDir = @"C:\Users";
			foreach (string userDir in Directory.GetDirectories(usersDir))
			{
				string userName = Path.GetFileName(userDir);
				if (userName == "Public" || userName == "Default" || userName.StartsWith("$")) continue;

				string configPath = Path.Combine(userDir, "AppData", "Local", "HoskerBackup", "config.json");
				if (File.Exists(configPath))
				{
					// Update Config.Load to accept a path (add this overload in Config.cs if missing)
					Config config = Config.Load(configPath);
					if (config.IsValid)
					{
						userConfigs.Add(config);
						_logger.LogInformation($"Loaded config for user: {userName}");
					}
				}
			}

			while (!stoppingToken.IsCancellationRequested)
			{
				DateTime now = DateTime.Now;
				foreach (Config config in userConfigs)
				{
					if (now.Hour == config.ScheduleHour && now.Minute == config.ScheduleMinute &&
						(config.LastRun == null || config.LastRun.Value.Date < now.Date))
					{
						try
						{
							Backup backup = new Backup(config);
							backup.RunBackup(); // Assumes RunBackup is your main method
							config.LastRun = now;
							config.Save(); // Save updated LastRun
							_logger.LogInformation("Backup completed successfully.");
						}
						catch (Exception ex)
						{
							_logger.LogError($"Backup failed: {ex.Message}");
						}
					}
				}
				await Task.Delay(60000, stoppingToken); // Check every 1 minute
			}
		}
	}
}