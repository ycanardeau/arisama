using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class MaritalStateMachineConfiguration : IEntityTypeConfiguration<MaritalStateMachine>
{
	private static readonly JsonSerializerOptions s_jsonSerializerOptions = new();

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

		builder.Property(x => x.States)
			.HasConversion(
				x => JsonSerializer.Serialize(x, s_jsonSerializerOptions),
				x => JsonSerializer.Deserialize<List<MaritalStatus>>(x, s_jsonSerializerOptions)!,
				new ValueComparer<ICollection<MaritalStatus>>(
					(c1, c2) => c1!.SequenceEqual(c2!),
					c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
					c => c.ToList()
				)
			);
	}
}
