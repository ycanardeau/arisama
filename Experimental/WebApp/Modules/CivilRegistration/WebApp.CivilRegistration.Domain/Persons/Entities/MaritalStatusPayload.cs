using WebApp.CivilRegistration.Domain.DeathCertificates.Entities;
using WebApp.CivilRegistration.Domain.DeathCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.DivorceCertificates.Entities;
using WebApp.CivilRegistration.Domain.DivorceCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal sealed class MarriageInformation
{
	public MarriageCertificateId MarriageCertificateId { get; set; }
	public required MarriageCertificate MarriageCertificate { get; set; }
	public required Age MarriedAtAge { get; set; }
	public PersonId MarriedWithId { get; set; }
	public required Person MarriedWith { get; set; }
}

internal sealed class DivorceInformation
{
	public DivorceCertificateId DivorceCertificateId { get; set; }
	public required DivorceCertificate DivorceCertificate { get; set; }
	public required Age DivorcedAtAge { get; set; }
	public PersonId DivorcedFromId { get; set; }
	public required Person DivorcedFrom { get; set; }
};

internal sealed class WidowhoodInformation
{
	public required Age WidowedAtAge { get; set; }
	public PersonId WidowedFromId { get; set; }
	public required Person WidowedFrom { get; set; }
}

internal sealed class DeathInformation
{
	public DeathCertificateId DeathCertificateId { get; set; }
	public required DeathCertificate DeathCertificate { get; set; }
	public required Age DeceasedAtAge { get; set; }
}

internal abstract class MaritalStatusPayload;

internal sealed class SingleStatePayload : MaritalStatusPayload;

internal sealed class MarriedStatePayload : MaritalStatusPayload
{
	public required MarriageInformation MarriageInformation { get; set; }
}

internal sealed class DivorcedStatePayload : MaritalStatusPayload
{
	public required MarriageInformation MarriageInformation { get; set; }
	public required DivorceInformation DivorceInformation { get; set; }
}

internal sealed class WidowedStatePayload : MaritalStatusPayload
{
	public required MarriageInformation MarriageInformation { get; set; }
	public required WidowhoodInformation WidowhoodInformation { get; set; }
}

internal sealed class DeceasedStatePayload : MaritalStatusPayload
{
	public required DeathInformation DeathInformation { get; set; }
}
