using Microsoft.Extensions.Hosting;
using WebApp.CivilRegistration.Application;
using WebApp.CivilRegistration.Endpoints;
using WebApp.CivilRegistration.Infrastructure;

namespace WebApp.CivilRegistration.Module;

internal static class ServiceExtensions
{
	public static IHostApplicationBuilder AddModule(this IHostApplicationBuilder builder)
	{
		builder.AddApplication();
		builder.AddInfrastructure();
		builder.Services.AddEndpoints();
		return builder;
	}
}
