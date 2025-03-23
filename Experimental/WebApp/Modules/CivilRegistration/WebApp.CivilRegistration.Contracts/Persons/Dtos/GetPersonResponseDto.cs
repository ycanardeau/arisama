namespace WebApp.CivilRegistration.Contracts.Persons.Dtos;

public sealed record MaritalStateMachineDto(int Version, MaritalStatusDto[] States);

public sealed record GetPersonResponseDto(MaritalStateMachineDto MaritalStateMachine);
