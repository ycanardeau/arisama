using System.Diagnostics;
using WebApp.CivilRegistration.Domain.DeathCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.DivorceCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.Persons.Entities;

namespace WebApp.CivilRegistration.Domain.Persons.ValueObjects;

internal sealed record MarriageInformation(
	MarriageCertificateId MarriageCertificateId,
	Age MarriedAtAge,
	PersonId MarriedWithId
);

internal sealed record DivorceInformation(
	DivorceCertificateId DivorceCertificateId,
	Age DivorcedAtAge,
	PersonId DivorcedFromId
);

internal sealed record WidowhoodInformation(
	Age WidowedAtAge,
	PersonId WidowedFromId
);

internal sealed record DeathInformation(
	DeathCertificateId DeathCertificateId,
	Age DeceasedAtAge
);

internal abstract record MaritalStatus : IMaritalStatus
{
	public MaritalStatusVersion Version { get; set; }
}

internal sealed record Single() : MaritalStatus
	, ICanDecease
	, ICanMarry
;

internal sealed record Married(MarriageInformation MarriageInformation) : MaritalStatus
	, IHasMarriageInformation
	, ICanDecease
	, ICanDivorce
	, ICanBecomeWidowed
;

internal sealed record Divorced(
	MarriageInformation MarriageInformation,
	DivorceInformation DivorceInformation
) : MaritalStatus
	, IHasMarriageInformation
	, IHasDivorceInformation
	, ICanDecease
	, ICanMarry
;

internal sealed record Widowed(
	MarriageInformation MarriageInformation,
	WidowhoodInformation WidowhoodInformation
) : MaritalStatus
	, IHasMarriageInformation
	, IHasWidowhoodInformation
	, ICanDecease
	, ICanMarry
;

internal sealed record Deceased(DeathInformation DeathInformation) : MaritalStatus
	, IHasDeathInformation
;

internal static class MaritalStatusExtensions
{
	public static U Match<U>(
		this MaritalStatus state,
		Func<Single, U> onSingle,
		Func<Married, U> onMarried,
		Func<Divorced, U> onDivorced,
		Func<Widowed, U> onWidowed,
		Func<Deceased, U> onDeceased
	)
	{
		return state switch
		{
			Single x => onSingle(x),
			Married x => onMarried(x),
			Divorced x => onDivorced(x),
			Widowed x => onWidowed(x),
			Deceased x => onDeceased(x),
			_ => throw new UnreachableException(),
		};
	}
}
