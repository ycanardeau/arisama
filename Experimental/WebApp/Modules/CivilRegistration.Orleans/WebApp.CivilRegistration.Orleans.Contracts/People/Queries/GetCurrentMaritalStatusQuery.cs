using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;

namespace WebApp.CivilRegistration.Orleans.Contracts.People.Queries;

public sealed record GetCurrentMaritalStatusQuery(int Id)
	: IRequest<Result<GetCurrentMaritalStatusResponseDto, CivilRegistrationOrleansError>>;
