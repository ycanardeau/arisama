using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Domain.People.Entities;

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
