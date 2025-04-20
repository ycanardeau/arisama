using FastEndpoints;
using FastEndpoints.Swagger;
using Orleans;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

WebApp.CivilRegistration.Module.ServiceExtensions.AddModule(builder);
WebApp.UrlShortener.Module.ServiceExtensions.AddModule(builder);

builder.Services
	.AddFastEndpoints()
	.SwaggerDocument(o =>
	{
		o.DocumentSettings = s =>
		{
			s.DocumentName = "v1";
		};
	})
	.AddMediatR(config =>
	{
		config.RegisterServicesFromAssemblyContaining<Program>();
	});

builder.UseOrleans(o => o.UseDashboard(options => { }));

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
	app.UseSwaggerGen();
}

app.Map("/dashboard", x => x.UseOrleansDashboard());

app.Run();
