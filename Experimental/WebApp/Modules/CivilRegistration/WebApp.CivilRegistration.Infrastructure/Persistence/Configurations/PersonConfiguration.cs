using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
	public void Configure(EntityTypeBuilder<Person> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasConversion(x => x.Value, x => new(x))
			.ValueGeneratedOnAdd();

		builder.Property(x => x.Gender)
			.HasMaxLength(255)
			.HasConversion(
				x => x is Male
					? "Male"
					: x is Female
					? "Female"
					: null,
				x => x == "Male"
					? new Male()
					: x == "Female"
					? new Female()
					: null!
			);

		builder.Property(x => x.Age)
			.HasConversion(x => x.Value, x => new(x));
	}
}
