using FastEndpoints;

namespace WebApp.UrlShortener.Endpoints.Urls;

internal class UrlsGroup : Group
{
	public UrlsGroup()
	{
		Configure("/urls", ep =>
		{
		});
	}
}
