using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Enums;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Application.Services.Mappers;

internal class PersonMapper(IMaritalStatusMapper maritalStatusMapper) : IPersonMapper
{
	public PersonDto Map(Person person)
	{
		return new PersonDto(
			Gender: person.Gender.Match(
				onMale: x => ApiGender.Male,
				onFemale: x => ApiGender.Female
			),
			Age: person.Age.Value,
			MaritalStateMachine: new MaritalStateMachineDto(
				Version: person.MaritalStateMachine.Version.Value,
				States: [
					.. person.MaritalStateMachine.States
						.OrderBy(x => x.Version)
						.Select(maritalStatusMapper.Map)
				]
			)
		);
	}
}
