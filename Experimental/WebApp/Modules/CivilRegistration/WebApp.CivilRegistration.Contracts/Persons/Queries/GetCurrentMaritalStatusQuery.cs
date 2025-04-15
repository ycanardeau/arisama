using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.Persons.Dtos;

namespace WebApp.CivilRegistration.Contracts.Persons.Queries;

public sealed record GetCurrentMaritalStatusQuery(int Id) : IRequest<Result<GetCurrentMaritalStatusResponseDto, InvalidOperationException>>;
