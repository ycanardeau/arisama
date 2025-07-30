using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using Single = WebApp.CivilRegistration.Domain.Persons.ValueObjects.Single;

namespace WebApp.CivilRegistration.Application.Services.Mappers;

internal class MaritalStatusMapper : IMaritalStatusMapper
{
	private static MarriageInformationDto Map(MarriageInformation value)
	{
		return new MarriageInformationDto(
			MarriageCertificateId: value.MarriageCertificateId.Value,
			MarriedAtAge: value.MarriedAtAge.Value,
			MarriedWithId: value.MarriedWithId.Value
		);
	}

	private static DivorceInformationDto Map(DivorceInformation value)
	{
		return new DivorceInformationDto(
			DivorceCertificateId: value.DivorceCertificateId.Value,
			DivorcedAtAge: value.DivorcedAtAge.Value,
			DivorcedFromId: value.DivorcedFromId.Value
		);
	}

	private static WidowhoodInformationDto Map(WidowhoodInformation value)
	{
		return new WidowhoodInformationDto(
			WidowedAtAge: value.WidowedAtAge.Value,
			WidowedFromId: value.WidowedFromId.Value
		);
	}

	private static DeathInformationDto Map(DeathInformation value)
	{
		return new DeathInformationDto(
			DeathCertificateId: value.DeathCertificateId.Value,
			DeceasedAtAge: value.DeceasedAtAge.Value
		);
	}

	private static MaritalStatusDto Map(Single value)
	{
		return new SingleDto
		{
			Version = value.Version.Value,
		};
	}

	private static MaritalStatusDto Map(Married value)
	{
		return new MarriedDto
		{
			Version = value.Version.Value,
			MarriageInformation = Map(value.MarriageInformation),
		};
	}

	private static MaritalStatusDto Map(Divorced value)
	{
		return new DivorcedDto
		{
			Version = value.Version.Value,
			MarriageInformation = Map(value.MarriageInformation),
			DivorceInformation = Map(value.DivorceInformation),
		};
	}

	private static MaritalStatusDto Map(Widowed value)
	{
		return new WidowedDto
		{
			Version = value.Version.Value,
			MarriageInformation = Map(value.MarriageInformation),
			WidowhoodInformation = Map(value.WidowhoodInformation),
		};
	}

	private static MaritalStatusDto Map(Deceased value)
	{
		return new DeceasedDto
		{
			Version = value.Version.Value,
			DeathInformation = Map(value.DeathInformation),
		};
	}

	public MaritalStatusDto Map(MaritalStatus value)
	{
		return value.Match(
			onSingle: Map,
			onMarried: Map,
			onDivorced: Map,
			onWidowed: Map,
			onDeceased: Map
		);
	}
}
