using Microsoft.EntityFrameworkCore;
using WebApp.CivilRegistration.Domain.Persons.Entities;

namespace WebApp.CivilRegistration.Infrastructure.Persistence;

internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public static string Schema { get; } = "WebApp_CivilRegistration";

	public DbSet<Person> Persons { get; set; }

	public DbSet<MaritalStateMachine> MaritalStateMachines { get; set; }

	public DbSet<MaritalStatus> MaritalStatuses { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema(Schema);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}
}
