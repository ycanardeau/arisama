using WebApp.CivilRegistration.Application.Interfaces.Mappers;
using WebApp.CivilRegistration.Contracts.People.Dtos;
using WebApp.CivilRegistration.Contracts.People.Enums;
using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Application.Services.Mappers;

internal class PersonMapper(IMaritalStatusMapper maritalStatusMapper) : IPersonMapper
{
	public PersonDto Map(Person person)
	{
		return new PersonDto(
			Id: person.Id.Value,
			Gender: person.Gender.Match(Male: x => ApiGender.Male, Female: x => ApiGender.Female),
			Age: person.Age.Value,
			MaritalStateMachine: new MaritalStateMachineDto(
				Version: person.MaritalStateMachine.Version.Value,
				States: [.. person.MaritalStateMachine.States.Select(maritalStatusMapper.Map)]
			)
		);
	}
}
