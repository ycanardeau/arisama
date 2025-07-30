using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal interface IMaritalStatus;

internal interface IHasMarriageInformation : IMaritalStatus
{
	MarriageInformation MarriageInformation { get; }
}

internal interface IHasDivorceInformation : IMaritalStatus
{
	DivorceInformation DivorceInformation { get; }
}

internal interface IHasWidowhoodInformation : IMaritalStatus
{
	WidowhoodInformation WidowhoodInformation { get; }
}

internal interface IHasDeathInformation : IMaritalStatus
{
	DeathInformation DeathInformation { get; }
}
