using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using static WebApp.CivilRegistration.Contracts.Persons.Dtos.MaritalStatusDto;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus;

namespace WebApp.CivilRegistration.Application.Services.Mappers;

internal class MaritalStatusMapper : IMaritalStatusMapper
{
	private static MaritalStatusDto Map(MaritalStatus.Single value)
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
		};
	}

	private static MaritalStatusDto Map(Divorced value)
	{
		return new DivorcedDto
		{
			Version = value.Version.Value,
		};
	}

	private static MaritalStatusDto Map(Widowed value)
	{
		return new WidowedDto
		{
			Version = value.Version.Value,
		};
	}

	public MaritalStatusDto Map(MaritalStatus value)
	{
		return value.Match(
			Map,
			Map,
			Map,
			Map
		);
	}
}
