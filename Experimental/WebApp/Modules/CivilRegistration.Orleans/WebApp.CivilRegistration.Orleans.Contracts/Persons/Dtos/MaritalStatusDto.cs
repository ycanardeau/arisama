using System.Text.Json.Serialization;

namespace WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;

public sealed record MarriageInformationDto(
	int MarriageCertificateId,
	int MarriedAtAge,
	int MarriedWithId
);

public sealed record DivorceInformationDto(
	int DivorceCertificateId,
	int DivorcedAtAge,
	int DivorcedFromId
);

public sealed record WidowhoodInformationDto(
	int WidowedAtAge,
	int WidowedFromId
);

public sealed record DeathInformationDto(
	int DeathCertificateId,
	int DeceasedAtAge
);

[JsonDerivedType(typeof(SingleStateDto), typeDiscriminator: "Single")]
[JsonDerivedType(typeof(MarriedStateDto), typeDiscriminator: "Married")]
[JsonDerivedType(typeof(DivorcedStateDto), typeDiscriminator: "Divorced")]
[JsonDerivedType(typeof(WidowedStateDto), typeDiscriminator: "Widowed")]
[JsonDerivedType(typeof(DeceasedStateDto), typeDiscriminator: "Deceased")]
public abstract record MaritalStatusDto
{
	public required int Version { get; init; }
}

public sealed record SingleStateDto : MaritalStatusDto;

public sealed record MarriedStateDto : MaritalStatusDto
{
	public required MarriageInformationDto MarriageInformation { get; init; }
}

public sealed record DivorcedStateDto : MaritalStatusDto
{
	public required MarriageInformationDto MarriageInformation { get; init; }
	public required DivorceInformationDto DivorceInformation { get; init; }
}

public sealed record WidowedStateDto : MaritalStatusDto
{
	public required MarriageInformationDto MarriageInformation { get; init; }
	public required WidowhoodInformationDto WidowhoodInformation { get; init; }
}

public sealed record DeceasedStateDto : MaritalStatusDto
{
	public required DeathInformationDto DeathInformation { get; init; }
}
