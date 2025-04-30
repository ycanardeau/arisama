using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Orleans.Contracts.Persons.Queries;

public sealed record GetPersonQuery(int Id) : IRequest<Result<GetPersonResponseDto>>;
