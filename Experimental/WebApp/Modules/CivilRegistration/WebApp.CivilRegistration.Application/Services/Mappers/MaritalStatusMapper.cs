using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

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

	private static MaritalStatusDto Map(MaritalStatus.Single value)
	{
		return new MaritalStatusDto.Single { };
	}

	private static MaritalStatusDto Map(MaritalStatus.Married value)
	{
		return new MaritalStatusDto.Married(MarriageInformation: Map(value.MarriageInformation));
	}

	private static MaritalStatusDto Map(MaritalStatus.Divorced value)
	{
		return new MaritalStatusDto.Divorced(
			MarriageInformation: Map(value.MarriageInformation),
			DivorceInformation: Map(value.DivorceInformation)
		);
	}

	private static MaritalStatusDto Map(MaritalStatus.Widowed value)
	{
		return new MaritalStatusDto.Widowed(
			MarriageInformation: Map(value.MarriageInformation),
			WidowhoodInformation: Map(value.WidowhoodInformation)
		);
	}

	private static MaritalStatusDto Map(MaritalStatus.Deceased value)
	{
		return new MaritalStatusDto.Deceased(DeathInformation: Map(value.DeathInformation));
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
