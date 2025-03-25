namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract record MaritalStatusPayload
{
	public sealed record SinglePayload : MaritalStatusPayload;

	public sealed record MarriedPayload : MaritalStatusPayload;

	public sealed record DivorcedPayload : MaritalStatusPayload;

	public sealed record WidowedPayload : MaritalStatusPayload;

	private MaritalStatusPayload() { }
}
