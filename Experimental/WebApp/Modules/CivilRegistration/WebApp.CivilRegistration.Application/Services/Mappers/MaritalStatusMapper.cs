using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using Single = WebApp.CivilRegistration.Domain.Persons.Entities.Single;

namespace WebApp.CivilRegistration.Application.Services.Mappers;

internal class MaritalStatusMapper : IMaritalStatusMapper
{
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
			MarriedAtAge = value.Payload.MarriedAtAge.Value,
			MarriedWithId = value.Payload.MarriedWithId.Value,
		};
	}

	private static MaritalStatusDto Map(Divorced value)
	{
		return new DivorcedDto
		{
			Version = value.Version.Value,
			DivorcedAtAge = value.Payload.DivorcedAtAge.Value,
			DivorcedFromId = value.Payload.DivorcedFromId.Value,
		};
	}

	private static MaritalStatusDto Map(Widowed value)
	{
		return new WidowedDto
		{
			Version = value.Version.Value,
			WidowedAtAge = value.Payload.WidowedAtAge.Value,
			WidowedFromId = value.Payload.WidowedFromId.Value,
		};
	}

	private static MaritalStatusDto Map(Deceased value)
	{
		return new DeceasedDto
		{
			Version = value.Version.Value,
			DeceasedAtAge = value.Payload.DeceasedAtAge.Value,
			WidowedId = value.Payload.WidowedId?.Value,
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
