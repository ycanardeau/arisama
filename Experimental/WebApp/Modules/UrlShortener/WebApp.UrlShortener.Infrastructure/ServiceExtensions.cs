using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using WebApp.UrlShortener.Infrastructure.Persistence;

namespace WebApp.UrlShortener.Infrastructure;

file interface IInfrastructure;

internal static class ServiceExtensions
{
	public static IServiceCollection AddDbContext(this IHostApplicationBuilder builder)
	{
		return builder.Services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseMySql(
				builder.Configuration.GetConnectionString("DefaultConnection"),
				MySqlServerVersion.LatestSupportedServerVersion,
				sqlOptions =>
				{
					sqlOptions
						// https://learn.microsoft.com/en-us/samples/dotnet/aspire-samples/aspire-efcore-migrations/
						.MigrationsAssembly("WebApp.UrlShortener.MigrationService")
						// https://www.milanjovanovic.tech/blog/using-multiple-ef-core-dbcontext-in-single-application
						.MigrationsHistoryTable(tableName: HistoryRepository.DefaultTableName, schema: ApplicationDbContext.Schema)
						// https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/pull/982#issue-532498042
						.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, entity) => $"{schema ?? "dbo"}_{entity}");
				}
			);
		});
	}

	public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
	{
		builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<IInfrastructure>());

		builder.AddDbContext();

		builder.UseOrleans(static siloBuilder =>
		{
			siloBuilder.UseLocalhostClustering();
			siloBuilder.AddMemoryGrainStorage("urls");
			siloBuilder.AddMemoryGrainStorage("robotStateStore");
			siloBuilder.AddMemoryGrainStorage("PubSubStore");
			siloBuilder.AddMemoryStreams("SMSProvider");
			siloBuilder.UseTransactions();
		});

		return builder;
	}
}
