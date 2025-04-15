using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using SingleState = WebApp.CivilRegistration.Domain.Persons.Entities.SingleState;

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

	private static MaritalStatusDto Map(SingleState value)
	{
		return new SingleStateDto
		{
			Version = value.Version.Value,
		};
	}

	private static MaritalStatusDto Map(MarriedState value)
	{
		return new MarriedStateDto
		{
			Version = value.Version.Value,
			MarriageInformation = Map(value.Payload.MarriageInformation),
		};
	}

	private static MaritalStatusDto Map(DivorcedState value)
	{
		return new DivorcedStateDto
		{
			Version = value.Version.Value,
			MarriageInformation = Map(value.Payload.MarriageInformation),
			DivorceInformation = Map(value.Payload.DivorceInformation),
		};
	}

	private static MaritalStatusDto Map(WidowedState value)
	{
		return new WidowedStateDto
		{
			Version = value.Version.Value,
			MarriageInformation = Map(value.Payload.MarriageInformation),
			WidowhoodInformation = Map(value.Payload.WidowhoodInformation),
		};
	}

	private static MaritalStatusDto Map(DeceasedState value)
	{
		return new DeceasedStateDto
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
