using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.Persons.Commands;

internal class CreatePersonCommandHandler(IGrainFactory grains)
	: IRequestHandler<CreatePersonCommand, Result<CreatePersonResponseDto, WebAppError>>
{
	public Task<Result<CreatePersonResponseDto, WebAppError>> Handle(
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
