using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatusPayload;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class MaritalStatusConfiguration : IEntityTypeConfiguration<MaritalStatus>
{
	public abstract class WithPayloadConfiguration<TState, TPayload> : IEntityTypeConfiguration<TState>
		where TPayload : MaritalStatusPayload
		where TState : WithPayload<TPayload>
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

	public class SingleConfiguration : WithPayloadConfiguration<MaritalStatus.Single, SinglePayload>;
	public class MarriedConfiguration : WithPayloadConfiguration<Married, MarriedPayload>;
	public class DivorcedConfiguration : WithPayloadConfiguration<Divorced, DivorcedPayload>;
	public class WidowedConfiguration : WithPayloadConfiguration<Widowed, WidowedPayload>;
}
