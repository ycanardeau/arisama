using System.Text.Json.Serialization;

namespace WebApp.CivilRegistration.Contracts.Persons.Dtos;

public sealed record MarriageInformationDto(
	Guid MarriageCertificateId,
	int MarriedAtAge,
	Guid MarriedWithId
);

public sealed record DivorceInformationDto(
	Guid DivorceCertificateId,
	int DivorcedAtAge,
	Guid DivorcedFromId
);

public sealed record WidowhoodInformationDto(int WidowedAtAge, Guid WidowedFromId);

public sealed record DeathInformationDto(Guid DeathCertificateId, int DeceasedAtAge);

[JsonDerivedType(typeof(Single), typeDiscriminator: nameof(Single))]
[JsonDerivedType(typeof(Married), typeDiscriminator: nameof(Married))]
[JsonDerivedType(typeof(Divorced), typeDiscriminator: nameof(Divorced))]
[JsonDerivedType(typeof(Widowed), typeDiscriminator: nameof(Widowed))]
[JsonDerivedType(typeof(Deceased), typeDiscriminator: nameof(Deceased))]
public abstract record MaritalStatusDto
{
	private MaritalStatusDto() { }

	public sealed record Single() : MaritalStatusDto;

	public sealed record Married(MarriageInformationDto MarriageInformation) : MaritalStatusDto;

	public sealed record Divorced(
		MarriageInformationDto MarriageInformation,
		DivorceInformationDto DivorceInformation
	) : MaritalStatusDto;

	public sealed record Widowed(
		MarriageInformationDto MarriageInformation,
		WidowhoodInformationDto WidowhoodInformation
	) : MaritalStatusDto;

	public sealed record Deceased(DeathInformationDto DeathInformation) : MaritalStatusDto;
}
