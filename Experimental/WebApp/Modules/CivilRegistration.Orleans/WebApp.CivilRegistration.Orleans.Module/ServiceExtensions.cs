using Microsoft.Extensions.Hosting;
using WebApp.CivilRegistration.Orleans.Application;
using WebApp.CivilRegistration.Orleans.Endpoints;
using WebApp.CivilRegistration.Orleans.Infrastructure;

namespace WebApp.CivilRegistration.Orleans.Module;

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
