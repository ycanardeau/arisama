using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract record MaritalStatusPayload;

internal sealed record SinglePayload : MaritalStatusPayload;

internal sealed record MarriedPayload(PersonId MarriedWithId) : MaritalStatusPayload;

internal sealed record DivorcedPayload(PersonId DivorcedFromId) : MaritalStatusPayload;

internal sealed record WidowedPayload : MaritalStatusPayload;
