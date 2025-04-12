using DiscriminatedOnions;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApp.UrlShortener.Contracts.Urls.Dtos;
using WebApp.UrlShortener.Contracts.Urls.Queries;

namespace WebApp.UrlShortener.Infrastructure.Integrations.Urls;

internal interface IUrlShortenerGrain : IGrainWithStringKey
{
	Task SetUrl(string fullUrl);

	Task<string> GetUrl();
}

[GenerateSerializer, Alias(nameof(UrlDetails))]
public sealed record UrlDetails
{
	[Id(0)]
	public string FullUrl { get; set; } = "";

	[Id(1)]
	public string ShortenedRouteSegment { get; set; } = "";
}

internal sealed class UrlShortenerGrain(
	[PersistentState(stateName: "url", storageName: "urls")]
	IPersistentState<UrlDetails> state
) : Grain, IUrlShortenerGrain
{
	public async Task SetUrl(string fullUrl)
	{
		state.State = new()
		{
			ShortenedRouteSegment = this.GetPrimaryKeyString(),
			FullUrl = fullUrl,
		};

		await state.WriteStateAsync();
	}

	public Task<string> GetUrl()
	{
		return Task.FromResult(state.State.FullUrl);
	}
}

internal class ShortenQueryHandler(
	IHttpContextAccessor httpContextAccessor,
	IGrainFactory grains
) : IRequestHandler<ShortenQuery, Result<ShortenResponseDto, InvalidOperationException>>
{
	private readonly HttpContext _httpContext = httpContextAccessor.HttpContext;

	public async Task<Result<ShortenResponseDto, InvalidOperationException>> Handle(ShortenQuery request, CancellationToken cancellationToken)
	{
		var host = $"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}";

		if (string.IsNullOrWhiteSpace(request.Url) || Uri.IsWellFormedUriString(request.Url, UriKind.Absolute) is false)
		{
			return Result.Error(new InvalidOperationException($"""
                The URL query string is required and needs to be well formed.
                Consider, ${host}/shorten?url=https://www.microsoft.com.
                """));
		}

		var shortenedRouteSegment = Guid.NewGuid().GetHashCode().ToString("X");

		var shortenerGrain = grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);

		await shortenerGrain.SetUrl(request.Url);

		var resultBuilder = new UriBuilder(host)
		{
			Path = $"/go/{shortenedRouteSegment}",
		};

		return Result.Ok(new ShortenResponseDto(resultBuilder.Uri));
	}
}
