using WebApp.CivilRegistration.Contracts.People.Dtos;

namespace WebApp.CivilRegistration.Contracts.People.Queries;

public sealed record GetPersonQuery(Guid Id)
	: IRequest<Result<GetPersonResponseDto, CivilRegistrationError>>;
