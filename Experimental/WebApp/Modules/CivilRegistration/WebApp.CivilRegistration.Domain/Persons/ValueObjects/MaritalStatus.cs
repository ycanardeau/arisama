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

[GenerateMatch]
[JsonDerivedType(typeof(SingleState), typeDiscriminator: "Single")]
[JsonDerivedType(typeof(MarriedState), typeDiscriminator: "Married")]
[JsonDerivedType(typeof(DivorcedState), typeDiscriminator: "Divorced")]
[JsonDerivedType(typeof(WidowedState), typeDiscriminator: "Widowed")]
[JsonDerivedType(typeof(DeceasedState), typeDiscriminator: "Deceased")]
internal abstract record MaritalStatus : IMaritalStatus;

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
