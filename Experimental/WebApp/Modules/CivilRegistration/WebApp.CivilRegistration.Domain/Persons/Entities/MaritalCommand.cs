namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract record MaritalCommand;

internal sealed record MarryCommand(Person MarryWith) : MaritalCommand;

internal sealed record DivorceCommand : MaritalCommand;

internal sealed record BecomeWidowedCommand : MaritalCommand;

internal sealed record DeceaseCommand : MaritalCommand;
