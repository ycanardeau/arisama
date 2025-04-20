using Orleans.Streams;
using WebApp.UrlShortener.Infrastructure.GrainInterfaces;

namespace WebApp.UrlShortener.Infrastructure.GrainClasses;

[ImplicitStreamSubscription("StartingInstruction")]
internal class SubscriberGrain : Grain, ISubscriberGrain, IAsyncObserver<InstructionMessage>
{
	public override async Task OnActivateAsync(CancellationToken cancellationToken)
	{
		var key = this.GetPrimaryKey();

		await this.GetStreamProvider("SMSProvider")
			.GetStream<InstructionMessage>("StartingInstruction", key)
			.SubscribeAsync(this);

		await base.OnActivateAsync(cancellationToken);
	}

	public Task OnErrorAsync(Exception ex)
	{
		return Task.CompletedTask;
	}

	public Task OnNextAsync(InstructionMessage item, StreamSequenceToken? token = null)
	{
		var msg = $"{item.Robot} starting \"{item.Instruction}\"";
		Console.WriteLine(msg);
		return Task.CompletedTask;
	}
}
