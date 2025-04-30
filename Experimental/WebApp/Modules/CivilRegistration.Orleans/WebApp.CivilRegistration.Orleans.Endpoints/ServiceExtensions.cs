using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.CivilRegistration.Orleans.Endpoints;

internal static class ServiceExtensions
{
	public static IServiceCollection AddEndpoints(this IServiceCollection services)
	{
		// TODO: services.AddFastEndpoints();
		return services;
	}
}
