using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Orleans.Contracts.Persons.Queries;

public sealed record ListPersonsQuery() : IRequest<Result<ListPersonsResponseDto, InvalidOperationException>>;
