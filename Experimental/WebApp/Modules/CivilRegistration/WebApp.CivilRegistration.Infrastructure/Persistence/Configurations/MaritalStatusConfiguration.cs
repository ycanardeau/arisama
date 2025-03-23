using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class MaritalStatusConfiguration : IEntityTypeConfiguration<MaritalStatus>
{
	public void Configure(EntityTypeBuilder<MaritalStatus> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasConversion(x => x.Value, x => new(x))
			.ValueGeneratedOnAdd();

		builder.HasOne(x => x.StateMachine)
			.WithMany(x => x.States)
			.HasForeignKey(x => x.StateMachineId)
			.IsRequired();

		builder.Property(x => x.Version)
			.HasConversion(x => x.Value, x => new(x));

		builder.HasIndex(x => new { x.StateMachineId, x.Version })
			.IsUnique();

		builder.HasDiscriminator()
			.HasValue<MaritalStatus.Single>("Single")
			.HasValue<Married>("Married")
			.HasValue<Divorced>("Divorced")
			.HasValue<Widowed>("Widowed")
			;
	}
}
