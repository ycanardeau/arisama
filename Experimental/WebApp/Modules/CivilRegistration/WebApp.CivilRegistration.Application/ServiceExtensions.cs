using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Application.Services.Mappers;

namespace WebApp.CivilRegistration.Application;

file interface IApplication;

internal static class ServiceExtensions
{
	public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
	{
		builder.Services.AddValidatorsFromAssemblyContaining<IApplication>(includeInternalTypes: true);

		builder.Services.AddScoped<IMaritalStatusMapper, MaritalStatusMapper>();
		builder.Services.AddScoped<IPersonMapper, PersonMapper>();

		return builder;
	}
}
