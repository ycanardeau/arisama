using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.Persons.Entities;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class MaritalStateMachineConfiguration : IEntityTypeConfiguration<MaritalStateMachine>
{
	public void Configure(EntityTypeBuilder<MaritalStateMachine> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasConversion(x => x.Value, x => new(x));

		builder.HasOne(x => x.Person)
			.WithOne(x => x.MaritalStateMachine)
			.HasForeignKey<MaritalStateMachine>(x => x.PersonId);

		builder.Property(x => x.Version)
			.HasConversion(x => x.Value, x => new(x));
	}
}
