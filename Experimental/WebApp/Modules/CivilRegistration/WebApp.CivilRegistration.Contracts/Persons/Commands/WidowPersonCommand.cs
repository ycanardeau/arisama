using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Contracts.Persons.Commands;

public sealed record WidowPersonCommand(int Id) : IRequest<Result<WidowPersonResponseDto, InvalidOperationException>>;
