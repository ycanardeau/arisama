using WebApp.CivilRegistration.Contracts.People.Enums;

namespace WebApp.CivilRegistration.Contracts.People.Dtos;

public sealed record MaritalStateMachineDto(int Version, MaritalStatusDto[] States);

public sealed record PersonDto(
	Guid Id,
	ApiGender Gender,
	int Age,
	MaritalStateMachineDto MaritalStateMachine
);
