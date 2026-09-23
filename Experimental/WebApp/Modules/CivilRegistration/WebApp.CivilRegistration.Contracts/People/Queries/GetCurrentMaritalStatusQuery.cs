using WebApp.CivilRegistration.Contracts.People.Dtos;

namespace WebApp.CivilRegistration.Contracts.People.Queries;

public sealed record GetCurrentMaritalStatusQuery(Guid Id)
	: IRequest<Result<GetCurrentMaritalStatusResponseDto, CivilRegistrationError>>;
