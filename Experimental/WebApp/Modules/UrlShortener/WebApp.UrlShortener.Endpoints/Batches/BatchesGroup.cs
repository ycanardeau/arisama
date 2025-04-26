using FastEndpoints;

namespace WebApp.UrlShortener.Endpoints.Batches;

internal class BatchesGroup : Group
{
	public BatchesGroup()
	{
		Configure("/batches", ep =>
		{
		});
	}
}
