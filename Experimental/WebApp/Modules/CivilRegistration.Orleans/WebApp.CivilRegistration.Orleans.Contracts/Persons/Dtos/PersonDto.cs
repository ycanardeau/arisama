using WebApp.CivilRegistration.Orleans.Contracts.Persons.Enums;

namespace WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;

public sealed record MaritalStateMachineDto(int Version, MaritalStatusDto[] States);

public sealed record PersonDto(
	int Id,
	ApiGender Gender,
	int Age,
	MaritalStateMachineDto MaritalStateMachine
);
