using System.Text.Json.Serialization;

namespace WebApp.CivilRegistration.Contracts.Persons.Dtos;

[JsonDerivedType(typeof(SingleDto), typeDiscriminator: "Single")]
[JsonDerivedType(typeof(MarriedDto), typeDiscriminator: "Married")]
[JsonDerivedType(typeof(DivorcedDto), typeDiscriminator: "Divorced")]
[JsonDerivedType(typeof(WidowedDto), typeDiscriminator: "Widowed")]
public abstract record MaritalStatusDto
{
	public required int Version { get; init; }

	private MaritalStatusDto() { }

	public sealed record SingleDto : MaritalStatusDto;

	public sealed record MarriedDto : MaritalStatusDto
	{
		public required int MarriedWithId { get; init; }
	}

	public sealed record DivorcedDto : MaritalStatusDto
	{
		public required int DivorcedFromId { get; init; }
	}

	public sealed record WidowedDto : MaritalStatusDto;
};
