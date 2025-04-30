using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Enums;

namespace WebApp.CivilRegistration.Orleans.Contracts.Persons.Commands;

public sealed record CreatePersonCommand(int Age, ApiGender Gender) : IRequest<Result<CreatePersonResponseDto, InvalidOperationException>>;
