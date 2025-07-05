using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Contracts.Persons.Enums;

namespace WebApp.CivilRegistration.Contracts.Persons.Commands;

public sealed record CreatePersonCommand(int Age, ApiGender Gender) : IRequest<Result<CreatePersonResponseDto>>;
