using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Commands;
using WebApp.CivilRegistration.Orleans.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Integrations.Persons.Commands;

internal class CreatePersonCommandHandler(IGrainFactory grains) : IRequestHandler<CreatePersonCommand, Result<CreatePersonResponseDto, InvalidOperationException>>
{
	public async Task<Result<CreatePersonResponseDto, InvalidOperationException>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
	{
		var personGrain = grains.GetGrain<IPersonGrain>(primaryKey: request.Id);

		await personGrain.Initialize();

		return Result.Ok(new CreatePersonResponseDto(Id: personGrain.GetPrimaryKeyString()));
	}
}
