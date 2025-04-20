using DiscriminatedOnions;
using MediatR;
using WebApp.UrlShortener.Contracts.Urls.Dtos;
using WebApp.UrlShortener.Contracts.Urls.Queries;

namespace WebApp.UrlShortener.Infrastructure.Integrations.Urls.Queries;

internal class GoQueryHandler(IGrainFactory grains) : IRequestHandler<GoQuery, Result<GoResponseDto, InvalidOperationException>>
{
	public async Task<Result<GoResponseDto, InvalidOperationException>> Handle(GoQuery request, CancellationToken cancellationToken)
	{
		var shortenerGrain = grains.GetGrain<IUrlShortenerGrain>(request.ShortenedRouteSegment);

		var url = await shortenerGrain.GetUrl();

		var redirectBuilder = new UriBuilder(url);

		return Result.Ok(new GoResponseDto(redirectBuilder.Uri.ToString()));
	}
}
