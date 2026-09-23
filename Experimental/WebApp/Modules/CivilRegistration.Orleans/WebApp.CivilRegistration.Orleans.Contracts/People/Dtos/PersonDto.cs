using WebApp.CivilRegistration.Orleans.Contracts.People.Enums;

namespace WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;

public sealed record MaritalStateMachineDto(int Version, MaritalStatusDto[] States);

public sealed record PersonDto(
	int Id,
	ApiGender Gender,
	int Age,
	MaritalStateMachineDto MaritalStateMachine
);
