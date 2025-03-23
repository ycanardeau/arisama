using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Contracts.Persons.Commands;

public sealed record MarryPersonCommand(int Id) : IRequest<Result<MarryPersonResponseDto, InvalidOperationException>>;
