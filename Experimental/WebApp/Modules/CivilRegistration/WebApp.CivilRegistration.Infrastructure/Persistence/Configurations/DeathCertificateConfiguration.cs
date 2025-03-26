using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.DeathCertificates.Entities;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class DeathCertificateConfiguration : IEntityTypeConfiguration<DeathCertificate>
{
	public void Configure(EntityTypeBuilder<DeathCertificate> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasConversion(x => x.Value, x => new(x))
			.ValueGeneratedOnAdd();

		builder.HasOne(x => x.Deceased)
			.WithMany()
			.HasForeignKey(x => x.DeceasedId)
			.IsRequired();
	}
}
