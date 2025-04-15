using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using SingleState = WebApp.CivilRegistration.Domain.Persons.Entities.SingleState;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class MaritalStatusConfiguration : IEntityTypeConfiguration<MaritalStatus>
{
	public void Configure(EntityTypeBuilder<MaritalStatus> builder)
	{
		builder.UseTptMappingStrategy();

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasConversion(x => x.Value, x => new(x))
			.ValueGeneratedOnAdd();

		builder.HasOne(x => x.StateMachine)
			.WithMany(x => x.States)
			.HasForeignKey(x => x.StateMachineId);

		builder.Property(x => x.Version)
			.HasConversion(x => x.Value, x => new(x));

		builder.HasIndex(x => new { x.StateMachineId, x.Version })
			.IsUnique();
	}
}

internal abstract class MaritalStatusConfiguration<TState, TPayload> : IEntityTypeConfiguration<TState>
	where TPayload : MaritalStatusPayload
	where TState : MaritalStatus<TPayload>
{
	public abstract void Configure(EntityTypeBuilder<TState> builder);
}

internal class SingleConfiguration : MaritalStatusConfiguration<SingleState, SingleStatePayload>
{
	public override void Configure(EntityTypeBuilder<SingleState> builder)
	{
		builder.OwnsOne(x => x.Payload, builder =>
		{
		});
	}
}

internal class MarriedConfiguration : MaritalStatusConfiguration<MarriedState, MarriedStatePayload>
{
	public override void Configure(EntityTypeBuilder<MarriedState> builder)
	{
		builder.OwnsOne(x => x.Payload, builder =>
		{
			builder.OwnsOne(x => x.MarriageInformation, builder =>
			{
				builder.HasOne(x => x.MarriageCertificate)
					.WithMany()
					.HasForeignKey(x => x.MarriageCertificateId);

				builder.Property(x => x.MarriedAtAge)
					.HasConversion(x => x.Value, x => new(x));

				builder.HasOne(x => x.MarriedWith)
					.WithMany()
					.HasForeignKey(x => x.MarriedWithId);
			});
		});
	}
}

internal class DivorcedConfiguration : MaritalStatusConfiguration<DivorcedState, DivorcedStatePayload>
{
	public override void Configure(EntityTypeBuilder<DivorcedState> builder)
	{
		builder.OwnsOne(x => x.Payload, builder =>
		{
			builder.OwnsOne(x => x.MarriageInformation, builder =>
			{
				builder.HasOne(x => x.MarriageCertificate)
					.WithMany()
					.HasForeignKey(x => x.MarriageCertificateId);

				builder.Property(x => x.MarriedAtAge)
					.HasConversion(x => x.Value, x => new(x));

				builder.HasOne(x => x.MarriedWith)
					.WithMany()
					.HasForeignKey(x => x.MarriedWithId);
			});

			builder.OwnsOne(x => x.DivorceInformation, builder =>
			{
				builder.HasOne(x => x.DivorceCertificate)
					.WithMany()
					.HasForeignKey(x => x.DivorceCertificateId);

				builder.Property(x => x.DivorcedAtAge)
					.HasConversion(x => x.Value, x => new(x));

				builder.HasOne(x => x.DivorcedFrom)
					.WithMany()
					.HasForeignKey(x => x.DivorcedFromId);
			});
		});
	}
}

internal class WidowedConfiguration : MaritalStatusConfiguration<WidowedState, WidowedStatePayload>
{
	public override void Configure(EntityTypeBuilder<WidowedState> builder)
	{
		builder.OwnsOne(x => x.Payload, builder =>
		{
			builder.OwnsOne(x => x.MarriageInformation, builder =>
			{
				builder.HasOne(x => x.MarriageCertificate)
					.WithMany()
					.HasForeignKey(x => x.MarriageCertificateId);

				builder.Property(x => x.MarriedAtAge)
					.HasConversion(x => x.Value, x => new(x));

				builder.HasOne(x => x.MarriedWith)
					.WithMany()
					.HasForeignKey(x => x.MarriedWithId);
			});

			builder.OwnsOne(x => x.WidowhoodInformation, builder =>
			{
				builder.Property(x => x.WidowedAtAge)
					.HasConversion(x => x.Value, x => new(x));

				builder.HasOne(x => x.WidowedFrom)
					.WithMany()
					.HasForeignKey(x => x.WidowedFromId);
			});
		});
	}
}

internal class DeceasedConfiguration : MaritalStatusConfiguration<DeceasedState, DeceasedStatePayload>
{
	public override void Configure(EntityTypeBuilder<DeceasedState> builder)
	{
		builder.OwnsOne(x => x.Payload, builder =>
		{
			builder.OwnsOne(x => x.DeathInformation, builder =>
			{
				builder.Property(x => x.DeathCertificateId)
					.HasConversion(x => x.Value, x => new(x));

				builder.Property(x => x.DeceasedAtAge)
					.HasConversion(x => x.Value, x => new(x));
			});
		});
	}
}
