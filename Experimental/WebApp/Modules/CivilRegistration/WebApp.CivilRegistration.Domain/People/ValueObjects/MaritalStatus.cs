using System.Text.Json.Serialization;
using WebApp.CivilRegistration.Domain.DeathCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.DivorceCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.Transitions;

namespace WebApp.CivilRegistration.Domain.People.ValueObjects;

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

internal sealed record WidowhoodInformation(Age WidowedAtAge, PersonId WidowedFromId);

internal sealed record DeathInformation(DeathCertificateId DeathCertificateId, Age DeceasedAtAge);

[GenerateMatch]
[JsonDerivedType(typeof(Single), typeDiscriminator: nameof(Single))]
[JsonDerivedType(typeof(Married), typeDiscriminator: nameof(Married))]
[JsonDerivedType(typeof(Divorced), typeDiscriminator: nameof(Divorced))]
[JsonDerivedType(typeof(Widowed), typeDiscriminator: nameof(Widowed))]
[JsonDerivedType(typeof(Deceased), typeDiscriminator: nameof(Deceased))]
internal abstract record MaritalStatus
{
	private MaritalStatus() { }

	public sealed record Single() : MaritalStatus, ICanDecease, ICanMarry;

	public sealed record Married(MarriageInformation MarriageInformation)
		: MaritalStatus,
			IHasMarriageInformation,
			ICanDecease,
			ICanDivorce,
			ICanBecomeWidowed;

	public sealed record Divorced(
		MarriageInformation MarriageInformation,
		DivorceInformation DivorceInformation
	) : MaritalStatus, IHasMarriageInformation, IHasDivorceInformation, ICanDecease, ICanMarry;

	public sealed record Widowed(
		MarriageInformation MarriageInformation,
		WidowhoodInformation WidowhoodInformation
	) : MaritalStatus, IHasMarriageInformation, IHasWidowhoodInformation, ICanDecease, ICanMarry;

	public sealed record Deceased(DeathInformation DeathInformation)
		: MaritalStatus,
			IHasDeathInformation;
}
