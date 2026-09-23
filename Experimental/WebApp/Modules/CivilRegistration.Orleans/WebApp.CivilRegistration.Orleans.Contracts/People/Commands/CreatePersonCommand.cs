using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;
using WebApp.CivilRegistration.Orleans.Contracts.People.Enums;

namespace WebApp.CivilRegistration.Orleans.Contracts.People.Commands;

public sealed record CreatePersonCommand(int Age, ApiGender Gender)
	: IRequest<Result<CreatePersonResponseDto, CivilRegistrationOrleansError>>;
