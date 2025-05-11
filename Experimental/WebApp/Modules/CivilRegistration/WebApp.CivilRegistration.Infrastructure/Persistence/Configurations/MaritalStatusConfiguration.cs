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
			.HasConversion(x => x.Value, x => new(x))
			.ValueGeneratedOnAdd();

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
	public abstract void Configure(EntityTypeBuilder<TState> builder);
}

internal class SingleConfiguration : MaritalStatusConfiguration<Single, SinglePayload>
{
	public override void Configure(EntityTypeBuilder<Single> builder)
	{
		builder.OwnsOne(x => x.Payload, builder =>
		{
		});
	}
}

internal class MarriedConfiguration : MaritalStatusConfiguration<Married, MarriedPayload>
{
	public override void Configure(EntityTypeBuilder<Married> builder)
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

internal class DivorcedConfiguration : MaritalStatusConfiguration<Divorced, DivorcedPayload>
{
	public override void Configure(EntityTypeBuilder<Divorced> builder)
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

internal class WidowedConfiguration : MaritalStatusConfiguration<Widowed, WidowedPayload>
{
	public override void Configure(EntityTypeBuilder<Widowed> builder)
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

internal class DeceasedConfiguration : MaritalStatusConfiguration<Deceased, DeceasedPayload>
{
	public override void Configure(EntityTypeBuilder<Deceased> builder)
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
