using DiscriminatedOnions;
using MediatR;
using WebApp.UrlShortener.Contracts.Robots.Dtos;
using WebApp.UrlShortener.Contracts.Robots.Queries;
using WebApp.UrlShortener.Infrastructure.GrainInterfaces;

namespace WebApp.UrlShortener.Infrastructure.Integrations.Robots.Queries;

internal class GetNextInstructionQueryHandler(IGrainFactory grains) : IRequestHandler<GetNextInstructionQuery, Result<GetNextInstructionResponseDto, InvalidOperationException>>
{
	public async Task<Result<GetNextInstructionResponseDto, InvalidOperationException>> Handle(GetNextInstructionQuery request, CancellationToken cancellationToken)
	{
		var grain = grains.GetGrain<IRobotGrain>(request.Id);
		var instruction = await grain.GetNextInstruction();
		return Result.Ok(new GetNextInstructionResponseDto(instruction));
	}
}
