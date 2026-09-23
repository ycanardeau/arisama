using WebApp.CivilRegistration.Contracts.People.Dtos;

namespace WebApp.CivilRegistration.Contracts.People.Queries;

public sealed record ListPeopleQuery()
	: IRequest<Result<ListPeopleResponseDto, CivilRegistrationError>>;
