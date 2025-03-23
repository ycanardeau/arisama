using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.CivilRegistration.Endpoints;

internal static class ServiceExtensions
{
	public static IServiceCollection AddEndpoints(this IServiceCollection services)
	{
		// TODO: services.AddFastEndpoints();
		return services;
	}
}
