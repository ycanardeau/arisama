using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.People.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.People.Dtos;
using WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.People.Commands;

internal class CreatePersonCommandHandler(IGrainFactory grains)
	: IRequestHandler<
		CreatePersonCommand,
		Result<CreatePersonResponseDto, CivilRegistrationOrleansError>
	>
{
	public Task<Result<CreatePersonResponseDto, CivilRegistrationOrleansError>> Handle(
		CreatePersonCommand request,
		CancellationToken cancellationToken
	)
	{
		var personGrain = grains.GetGrain<IPersonGrain>(Guid.CreateVersion7());

		return personGrain
			.Initialize()
			.FlatMap(_ => Ok(new CreatePersonResponseDto(Id: personGrain.GetPrimaryKey())));
	}
}
