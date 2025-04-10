using WebApp.CivilRegistration.Domain.DeathCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.DivorceCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract record MaritalStatusPayload;

internal sealed record SinglePayload : MaritalStatusPayload;

internal sealed record MarriedPayload(
	MarriageCertificateId MarriageCertificateId,
	Age MarriedAtAge,
	PersonId MarriedWithId
) : MaritalStatusPayload;

internal sealed record DivorcedPayload(
	DivorceCertificateId DivorceCertificateId,
	Age DivorcedAtAge,
	PersonId DivorcedFromId
) : MaritalStatusPayload;

internal sealed record WidowedPayload(
	Age WidowedAtAge,
	PersonId WidowedFromId
) : MaritalStatusPayload;

internal sealed record DeceasedPayload(
	DeathCertificateId DeathCertificateId,
	Age DeceasedAtAge,
	PersonId? WidowedId
) : MaritalStatusPayload;
