using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using Single = WebApp.CivilRegistration.Domain.Persons.Entities.Single;

namespace WebApp.CivilRegistration.Application.Services.Mappers;

internal class MaritalStatusMapper : IMaritalStatusMapper
{
	private static MarriageInformationDto Map(MarriageInformation value)
	{
		return new MarriageInformationDto(
			MarriageCertificateGuid: value.MarriageCertificateGuid.Value,
			MarriedAtAge: value.MarriedAtAge.Value,
			MarriedWithId: value.MarriedWithId.Value
		);
	}

	private static DivorceInformationDto Map(DivorceInformation value)
	{
		return new DivorceInformationDto(
			DivorceCertificateGuid: value.DivorceCertificateGuid.Value,
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
			DeathCertificateGuid: value.DeathCertificateGuid.Value,
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
			MarriageInformation = Map(value.Payload.MarriageInformation),
		};
	}

	private static MaritalStatusDto Map(Divorced value)
	{
		return new DivorcedDto
		{
			Version = value.Version.Value,
			MarriageInformation = Map(value.Payload.MarriageInformation),
			DivorceInformation = Map(value.Payload.DivorceInformation),
		};
	}

	private static MaritalStatusDto Map(Widowed value)
	{
		return new WidowedDto
		{
			Version = value.Version.Value,
			MarriageInformation = Map(value.Payload.MarriageInformation),
			WidowhoodInformation = Map(value.Payload.WidowhoodInformation),
		};
	}

	private static MaritalStatusDto Map(Deceased value)
	{
		return new DeceasedDto
		{
			Version = value.Version.Value,
			DeathInformation = Map(value.Payload.DeathInformation),
		};
	}

	public MaritalStatusDto Map(MaritalStatus value)
	{
		return value.Match(
			Map,
			Map,
			Map,
			Map,
			Map
		);
	}
}
