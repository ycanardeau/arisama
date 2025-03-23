namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract record MaritalCommand
{
	public sealed record MarryCommand : MaritalCommand;

	public sealed record DivorceCommand : MaritalCommand;

	public sealed record BecomeWidowedCommand : MaritalCommand;
}
