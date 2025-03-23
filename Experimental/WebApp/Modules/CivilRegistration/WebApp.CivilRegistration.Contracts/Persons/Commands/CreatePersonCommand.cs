using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Contracts.Persons.Commands;

public sealed record CreatePersonCommand() : IRequest<Result<CreatePersonResponseDto, InvalidOperationException>>;
