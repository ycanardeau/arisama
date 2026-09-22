namespace WebApp.CivilRegistration.Infrastructure.Tests;

// A no-op IMediator so ApplicationDbContext.SaveChangesAsync can dispatch domain events
// without a real handler pipeline. Only Publish is exercised by the tests.
internal sealed class NoopMediator : IMediator
{
	public Task Publish(object notification, CancellationToken cancellationToken = default) =>
		Task.CompletedTask;

	public Task Publish<TNotification>(
		TNotification notification,
		CancellationToken cancellationToken = default
	)
		where TNotification : INotification => Task.CompletedTask;

	public Task<TResponse> Send<TResponse>(
		IRequest<TResponse> request,
		CancellationToken cancellationToken = default
	) => throw new NotSupportedException();

	public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
		where TRequest : IRequest => throw new NotSupportedException();

	public Task<object?> Send(object request, CancellationToken cancellationToken = default) =>
		throw new NotSupportedException();

	public IAsyncEnumerable<TResponse> CreateStream<TResponse>(
		IStreamRequest<TResponse> request,
		CancellationToken cancellationToken = default
	) => throw new NotSupportedException();

	public IAsyncEnumerable<object?> CreateStream(
		object request,
		CancellationToken cancellationToken = default
	) => throw new NotSupportedException();
}
