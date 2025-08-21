/*
 * using HoskerBackupService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
*/

using HoskerBackupService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging.EventLog;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

// Configure for Windows Service
builder.Services.AddWindowsService(options =>
{
	options.ServiceName = "HoskerBackupService";
});

// Add Event Log logging
builder.Logging.AddEventLog(new EventLogSettings
{
	SourceName = "HoskerBackupService"
});

var host = builder.Build();
host.Run();