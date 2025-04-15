using System.Text.Json.Serialization;

namespace WebApp.CivilRegistration.Contracts.Persons.Dtos;

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

[JsonDerivedType(typeof(SingleDto), typeDiscriminator: "Single")]
[JsonDerivedType(typeof(MarriedDto), typeDiscriminator: "Married")]
[JsonDerivedType(typeof(DivorcedDto), typeDiscriminator: "Divorced")]
[JsonDerivedType(typeof(WidowedDto), typeDiscriminator: "Widowed")]
[JsonDerivedType(typeof(DeceasedDto), typeDiscriminator: "Deceased")]
public abstract record MaritalStatusDto
{
	public required int Version { get; init; }
}

public sealed record SingleDto : MaritalStatusDto;

public sealed record MarriedDto : MaritalStatusDto
{
	public required MarriageInformationDto MarriageInformation { get; init; }
}

public sealed record DivorcedDto : MaritalStatusDto
{
	public required MarriageInformationDto MarriageInformation { get; init; }
	public required DivorceInformationDto DivorceInformation { get; init; }
}

public sealed record WidowedDto : MaritalStatusDto
{
	public required MarriageInformationDto MarriageInformation { get; init; }
	public required WidowhoodInformationDto WidowhoodInformation { get; init; }
}

public sealed record DeceasedDto : MaritalStatusDto
{
	public required DeathInformationDto DeathInformation { get; init; }
}
