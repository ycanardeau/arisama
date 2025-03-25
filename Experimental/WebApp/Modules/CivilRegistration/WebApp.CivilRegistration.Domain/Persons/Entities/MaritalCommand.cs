namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract record MaritalCommand
{
	public sealed record MarryCommand(Person MarryWith) : MaritalCommand;

	public sealed record DivorceCommand : MaritalCommand;

	public sealed record BecomeWidowedCommand : MaritalCommand;
}
