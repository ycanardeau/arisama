using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal interface IHasMarriageInformation
{
	MarriageInformation MarriageInformation { get; }
}

internal interface IHasDivorceInformation
{
	DivorceInformation DivorceInformation { get; }
}

internal interface IHasWidowhoodInformation
{
	WidowhoodInformation WidowhoodInformation { get; }
}

internal interface IHasDeathInformation
{
	DeathInformation DeathInformation { get; }
}
