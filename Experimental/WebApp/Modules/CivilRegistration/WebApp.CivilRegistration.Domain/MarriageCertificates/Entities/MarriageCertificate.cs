using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;

internal class MarriageCertificate : Entity<MarriageCertificateId>
{
	public PersonId HusbandId { get; set; }
	public required Person Husband { get; set; }
	public PersonId WifeId { get; set; }
	public required Person Wife { get; set; }

	private MarriageCertificate() { }

	private Result<MarriageCertificate, CivilRegistrationError> Marry()
	{
		return Husband == Wife ? new CivilRegistrationError.SameIndividual()
			: Husband.Gender == Wife.Gender ? new CivilRegistrationError.SameSexMarriage()
			: !Husband.CanBeHusband ? new CivilRegistrationError.IneligibleHusband()
			: !Wife.CanBeWife ? new CivilRegistrationError.IneligibleWife()
			: Husband
				.Marry(new MarryCommand(this, Wife))
				.Map(x => Wife.Marry(new MarryCommand(this, Husband)))
				.Map(x => this);
	}

	public static Result<MarriageCertificate, CivilRegistrationError> Create(CreateCommand command)
	{
		var marriageCertificate = new MarriageCertificate
		{
			Id = MarriageCertificateId.CreateVersion7(),
			Husband = command.Husband,
			Wife = command.Wife,
		};

		return marriageCertificate.Marry();
	}
}
