using WebApp.CivilRegistration.Contracts.People.Dtos;
using WebApp.CivilRegistration.Contracts.People.Enums;

namespace WebApp.CivilRegistration.Contracts.People.Commands;

public sealed record CreatePersonCommand(int Age, ApiGender Gender)
	: IRequest<Result<CreatePersonResponseDto, CivilRegistrationError>>;
