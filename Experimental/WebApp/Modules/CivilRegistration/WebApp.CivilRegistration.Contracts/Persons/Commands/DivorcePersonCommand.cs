using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Contracts.Persons.Commands;

public sealed record DivorcePersonCommand(int Id) : IRequest<Result<DivorcePersonResponseDto, InvalidOperationException>>;
