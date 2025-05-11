using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class MarriageCertificateConfiguration : IEntityTypeConfiguration<MarriageCertificate>
{
	public void Configure(EntityTypeBuilder<MarriageCertificate> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasConversion(x => x.Value, x => new(x));

		builder.HasOne(x => x.Husband)
			.WithMany()
			.HasForeignKey(x => x.HusbandId);

		builder.HasOne(x => x.Wife)
			.WithMany()
			.HasForeignKey(x => x.WifeId);
	}
}
