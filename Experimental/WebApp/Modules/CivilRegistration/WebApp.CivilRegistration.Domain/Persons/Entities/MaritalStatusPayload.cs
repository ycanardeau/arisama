using WebApp.CivilRegistration.Domain.DeathCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.DivorceCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal sealed record MarriageInformation(
	MarriageCertificateGuid MarriageCertificateGuid,
	Age MarriedAtAge,
	PersonId MarriedWithId
);

internal sealed record DivorceInformation(
	DivorceCertificateGuid DivorceCertificateGuid,
	Age DivorcedAtAge,
	PersonId DivorcedFromId
);

internal sealed record WidowhoodInformation(
	Age WidowedAtAge,
	PersonId WidowedFromId
);

internal sealed record DeathInformation(
	DeathCertificateGuid DeathCertificateGuid,
	Age DeceasedAtAge
);

internal abstract record MaritalStatusPayload;

internal sealed record SinglePayload : MaritalStatusPayload;

internal sealed record MarriedPayload(MarriageInformation MarriageInformation) : MaritalStatusPayload;

internal sealed record DivorcedPayload(
	MarriageInformation MarriageInformation,
	DivorceInformation DivorceInformation
) : MaritalStatusPayload;

internal sealed record WidowedPayload(
	MarriageInformation MarriageInformation,
	WidowhoodInformation WidowhoodInformation
) : MaritalStatusPayload;

internal sealed record DeceasedPayload(DeathInformation DeathInformation) : MaritalStatusPayload;
