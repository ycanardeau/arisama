using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Contracts.Persons.Queries;

public sealed record ListPersonsQuery() : IRequest<Result<ListPersonsResponseDto, InvalidOperationException>>;
