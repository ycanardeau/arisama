using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;

namespace WebApp.CivilRegistration.Orleans.Contracts.People.Queries;

public sealed record ListPeopleQuery()
	: IRequest<Result<ListPeopleResponseDto, CivilRegistrationOrleansError>>;
