using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Contracts.Persons.Queries;

public sealed record GetCurrentMaritalStatusQuery(Guid Id) : IRequest<Result<GetCurrentMaritalStatusResponseDto>>;
