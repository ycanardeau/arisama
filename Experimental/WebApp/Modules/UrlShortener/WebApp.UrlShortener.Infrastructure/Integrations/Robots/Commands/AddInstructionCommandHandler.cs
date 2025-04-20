using DiscriminatedOnions;
using MediatR;
using WebApp.UrlShortener.Contracts.Robots.Commands;
using WebApp.UrlShortener.Contracts.Robots.Dtos;
using WebApp.UrlShortener.Infrastructure.GrainInterfaces;

namespace WebApp.UrlShortener.Infrastructure.Integrations.Robots.Commands;

internal class AddInstructionCommandHandler(IGrainFactory grains) : IRequestHandler<AddInstructionCommand, Result<AddInstructionResponseDto, InvalidOperationException>>
{
	public async Task<Result<AddInstructionResponseDto, InvalidOperationException>> Handle(AddInstructionCommand request, CancellationToken cancellationToken)
	{
		var grain = grains.GetGrain<IRobotGrain>(request.Id);
		await grain.AddInstruction(request.Instruction);
		var count = await grain.GetInstructionCount();
		return Result.Ok(new AddInstructionResponseDto(count));
	}
}
