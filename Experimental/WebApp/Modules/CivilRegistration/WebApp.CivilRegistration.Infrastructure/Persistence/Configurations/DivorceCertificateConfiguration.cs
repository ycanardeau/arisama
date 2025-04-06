using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.CivilRegistration.Domain.DivorceCertificates.Entities;

namespace WebApp.CivilRegistration.Infrastructure.Persistence.Configurations;

internal class DivorceCertificateConfiguration : IEntityTypeConfiguration<DivorceCertificate>
{
	public void Configure(EntityTypeBuilder<DivorceCertificate> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasConversion(x => x.Value, x => new(x))
			.ValueGeneratedOnAdd();

		builder.HasOne(x => x.MarriageCertificate)
			.WithMany()
			.HasForeignKey(x => x.MarriageCertificateId);
	}
}
