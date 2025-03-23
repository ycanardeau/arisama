using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Contracts.Persons.Queries;

public sealed record GetPersonQuery(int Id) : IRequest<Result<GetPersonResponseDto, InvalidOperationException>>;
