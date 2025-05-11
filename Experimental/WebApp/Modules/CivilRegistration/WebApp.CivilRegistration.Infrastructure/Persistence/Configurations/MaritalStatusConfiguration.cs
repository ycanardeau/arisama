using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using Single = WebApp.CivilRegistration.Domain.Persons.Entities.Single;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class MaritalStatusConfiguration : IEntityTypeConfiguration<MaritalStatus>
{
	public void Configure(EntityTypeBuilder<MaritalStatus> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasConversion(x => x.Value, x => new(x));

		builder.HasOne(x => x.StateMachine)
			.WithMany(x => x.States)
			.HasForeignKey(x => x.StateMachineId);

		builder.Property(x => x.Version)
			.HasConversion(x => x.Value, x => new(x));

		builder.HasIndex(x => new { x.StateMachineId, x.Version })
			.IsUnique();

		builder.HasDiscriminator()
			.HasValue<Single>("Single")
			.HasValue<Married>("Married")
			.HasValue<Divorced>("Divorced")
			.HasValue<Widowed>("Widowed")
			;
	}
}

internal abstract class MaritalStatusConfiguration<TState, TPayload> : IEntityTypeConfiguration<TState>
	where TPayload : MaritalStatusPayload
	where TState : MaritalStatus<TPayload>
{
	private static readonly JsonSerializerOptions s_jsonSerializerOptions = new();

	public void Configure(EntityTypeBuilder<TState> builder)
	{
		builder.Property(x => x.Payload)
			.HasColumnName("Payload")
			.HasConversion(
				x => JsonSerializer.Serialize(x, s_jsonSerializerOptions),
				x => JsonSerializer.Deserialize<TPayload>(x, s_jsonSerializerOptions)!
			);
	}
}

internal class SingleConfiguration : MaritalStatusConfiguration<Single, SinglePayload>;
internal class MarriedConfiguration : MaritalStatusConfiguration<Married, MarriedPayload>;
internal class DivorcedConfiguration : MaritalStatusConfiguration<Divorced, DivorcedPayload>;
internal class WidowedConfiguration : MaritalStatusConfiguration<Widowed, WidowedPayload>;
internal class DeceasedConfiguration : MaritalStatusConfiguration<Deceased, DeceasedPayload>;
