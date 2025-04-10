using DiscriminatedOnions;
using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Domain.DivorceCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.Persons.Entities;

namespace WebApp.CivilRegistration.Domain.DivorceCertificates.Entities;

internal class DivorceCertificate : Entity<DivorceCertificateId>
{
	public MarriageCertificateId MarriageCertificateId { get; set; }
	public required MarriageCertificate MarriageCertificate { get; set; }

	private DivorceCertificate() { }

	private Result<DivorceCertificate, InvalidOperationException> Divorce()
	{
		return MarriageCertificate.Person1.Divorce(new DivorceCommand())
			.Map(x => MarriageCertificate.Person2.Divorce(new DivorceCommand()))
			.Map(x => this);
	}

	public static Result<DivorceCertificate, InvalidOperationException> Create(CreateCommand command)
	{
		var divorceCertificate = new DivorceCertificate
		{
			MarriageCertificate = command.MarriageCertificate,
		};

		return divorceCertificate.Divorce();
	}

}
