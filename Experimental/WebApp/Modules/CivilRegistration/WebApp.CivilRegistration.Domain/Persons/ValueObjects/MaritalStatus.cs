using System.Diagnostics;
using System.Text.Json.Serialization;
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

[JsonDerivedType(typeof(SingleState), typeDiscriminator: "Single")]
[JsonDerivedType(typeof(MarriedState), typeDiscriminator: "Married")]
[JsonDerivedType(typeof(DivorcedState), typeDiscriminator: "Divorced")]
[JsonDerivedType(typeof(WidowedState), typeDiscriminator: "Widowed")]
[JsonDerivedType(typeof(DeceasedState), typeDiscriminator: "Deceased")]
internal abstract record MaritalStatus : IMaritalStatus
{
	public MaritalStatusVersion Version { get; set; }
}

internal sealed record SingleState() : MaritalStatus
	, ICanDecease
	, ICanMarry
;

internal sealed record MarriedState(MarriageInformation MarriageInformation) : MaritalStatus
	, IHasMarriageInformation
	, ICanDecease
	, ICanDivorce
	, ICanBecomeWidowed
;

internal sealed record DivorcedState(
	MarriageInformation MarriageInformation,
	DivorceInformation DivorceInformation
) : MaritalStatus
	, IHasMarriageInformation
	, IHasDivorceInformation
	, ICanDecease
	, ICanMarry
;

internal sealed record WidowedState(
	MarriageInformation MarriageInformation,
	WidowhoodInformation WidowhoodInformation
) : MaritalStatus
	, IHasMarriageInformation
	, IHasWidowhoodInformation
	, ICanDecease
	, ICanMarry
;

internal sealed record DeceasedState(DeathInformation DeathInformation) : MaritalStatus
	, IHasDeathInformation
;

internal static class MaritalStatusExtensions
{
	public static U Match<U>(
		this MaritalStatus state,
		Func<SingleState, U> onSingle,
		Func<MarriedState, U> onMarried,
		Func<DivorcedState, U> onDivorced,
		Func<WidowedState, U> onWidowed,
		Func<DeceasedState, U> onDeceased
	)
	{
		return state switch
		{
			SingleState x => onSingle(x),
			MarriedState x => onMarried(x),
			DivorcedState x => onDivorced(x),
			WidowedState x => onWidowed(x),
			DeceasedState x => onDeceased(x),
			_ => throw new UnreachableException(),
		};
	}
}
