using FastEndpoints;

namespace WebApp.UrlShortener.Endpoints.Robots;

internal class RobotsGroup : Group
{
	public RobotsGroup()
	{
		Configure("/robots", ep =>
		{
		});
	}
}
