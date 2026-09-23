using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;

namespace WebApp.CivilRegistration.Orleans.Contracts.People.Queries;

public sealed record GetPersonQuery(int Id)
	: IRequest<Result<GetPersonResponseDto, CivilRegistrationOrleansError>>;
