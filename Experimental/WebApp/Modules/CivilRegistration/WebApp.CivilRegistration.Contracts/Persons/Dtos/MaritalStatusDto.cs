using System.Text.Json.Serialization;

namespace WebApp.CivilRegistration.Contracts.Persons.Dtos;

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
	public required int MarriedAtAge { get; init; }
	public required int MarriedWithId { get; init; }
}

public sealed record DivorcedDto : MaritalStatusDto
{
	public required int DivorcedAtAge { get; init; }
	public required int DivorcedFromId { get; init; }
}

public sealed record WidowedDto : MaritalStatusDto
{
	public required int WidowedAtAge { get; init; }
	public required int WidowedFromId { get; init; }
}

public sealed record DeceasedDto : MaritalStatusDto
{
	public required int DeceasedAtAge { get; init;}
}
