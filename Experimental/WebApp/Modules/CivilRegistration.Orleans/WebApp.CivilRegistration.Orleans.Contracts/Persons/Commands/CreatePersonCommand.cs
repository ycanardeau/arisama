using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Enums;

namespace WebApp.CivilRegistration.Orleans.Contracts.Persons.Commands;

public sealed record CreatePersonCommand(
	string Id,
	int Age,
	ApiGender Gender
) : IRequest<Result<CreatePersonResponseDto>>;
