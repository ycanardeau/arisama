using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract record MaritalStatusPayload
{
	public sealed record SinglePayload : MaritalStatusPayload;

	public sealed record MarriedPayload(PersonId MarriedWithId) : MaritalStatusPayload;

	public sealed record DivorcedPayload(PersonId DivorcedFromId) : MaritalStatusPayload;

	public sealed record WidowedPayload : MaritalStatusPayload;

	private MaritalStatusPayload() { }
}
