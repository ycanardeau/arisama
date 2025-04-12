using Microsoft.Extensions.Hosting;
using WebApp.UrlShortener.Application;
using WebApp.UrlShortener.Endpoints;
using WebApp.UrlShortener.Infrastructure;

namespace WebApp.UrlShortener.Module;

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
