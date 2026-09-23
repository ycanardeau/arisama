using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
	public void Configure(EntityTypeBuilder<Person> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id).HasConversion(x => x.Value, x => new(x));

		builder
			.Property(x => x.Gender)
			.HasMaxLength(255)
			.HasConversion(
				x => x.Match(onMale: _ => "Male", onFemale: _ => "Female"),
				x =>
					x == "Male" ? new Gender.Male()
					: x == "Female" ? new Gender.Female()
					: null!
			);

		builder.Property(x => x.Age).HasConversion(x => x.Value, x => new(x));
	}
}
