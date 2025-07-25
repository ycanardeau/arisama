using Nut.Results;
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

	private Result<DivorceCertificate> Divorce()
	{
		return MarriageCertificate.Husband.Divorce(new DivorceCommand(this))
			.Map(x => MarriageCertificate.Wife.Divorce(new DivorceCommand(this)))
			.Map(x => this);
	}

	public static Result<DivorceCertificate> Create(CreateCommand command)
	{
		var divorceCertificate = new DivorceCertificate
		{
			Id = DivorceCertificateId.CreateVersion7(),
			MarriageCertificate = command.MarriageCertificate,
		};

		return divorceCertificate.Divorce();
	}

}
