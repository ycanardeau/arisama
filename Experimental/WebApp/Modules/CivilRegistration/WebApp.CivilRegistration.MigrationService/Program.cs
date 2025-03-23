using WebApp.CivilRegistration.MigrationService;
using WebApp.CivilRegistration.Module;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.AddModule();

var host = builder.Build();
host.Run();
