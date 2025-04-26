using DiscriminatedOnions;
using MediatR;
using WebApp.UrlShortener.Contracts.Batches.Commands;
using WebApp.UrlShortener.Contracts.Batches.Dtos;
using WebApp.UrlShortener.Infrastructure.GrainInterfaces;

namespace WebApp.UrlShortener.Infrastructure.Integrations.Batches.Commands;

internal class AddInstructionsCommandHandler(IGrainFactory grains) : IRequestHandler<AddInstructionsCommand, Result<AddInstructionsResponseDto, InvalidOperationException>>
{
	public async Task<Result<AddInstructionsResponseDto, InvalidOperationException>> Handle(AddInstructionsCommand request, CancellationToken cancellationToken)
	{
		var grain = grains.GetGrain<IBatchGrain>(0);

		var input = request.Values
			.Select(keyValue => (keyValue.Id, keyValue.Instruction))
			.ToArray();

		await grain.AddInstructions(input);

		return Result.Ok(new AddInstructionsResponseDto());
	}
}
