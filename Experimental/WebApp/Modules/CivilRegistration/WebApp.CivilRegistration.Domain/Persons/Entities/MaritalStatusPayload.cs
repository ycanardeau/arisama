using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract record MaritalStatusPayload;

internal sealed record SinglePayload : MaritalStatusPayload;

internal sealed record MarriedPayload(
	Age MarriedAtAge,
	PersonId MarriedWithId
) : MaritalStatusPayload;

internal sealed record DivorcedPayload(
	Age DivorcedAtAge,
	PersonId DivorcedFromId
) : MaritalStatusPayload;

internal sealed record WidowedPayload(
	Age WidowedAtAge,
	PersonId WidowedFromId
) : MaritalStatusPayload;

internal sealed record DeceasedPayload(Age DeceasedAtAge) : MaritalStatusPayload;
