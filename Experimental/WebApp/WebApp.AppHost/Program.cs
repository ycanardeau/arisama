var builder = DistributedApplication.CreateBuilder(args);

var webAppDb = builder.AddMySql("mysql")
	.WithDataVolume("VolumeMount.mysql.data")
	.WithPhpMyAdmin()
	.AddDatabase("DefaultConnection", "WebApp");

var civilRegistrationMigrationService = builder.AddProject<Projects.WebApp_CivilRegistration_MigrationService>("webapp-civilregistration-migrationservice")
	.WithReference(webAppDb)
	.WaitFor(webAppDb);

builder.AddProject<Projects.WebApp>("webapp")
	.WithReference(webAppDb)
	.WaitForCompletion(civilRegistrationMigrationService);

builder.Build().Run();
