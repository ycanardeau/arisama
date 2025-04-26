using Orleans.Concurrency;
using WebApp.UrlShortener.Infrastructure.GrainInterfaces;

namespace WebApp.UrlShortener.Infrastructure.GrainClasses;

[StatelessWorker]
internal class BatchGrain : Grain, IBatchGrain
{
	public Task AddInstructions((string Id, string Instruction)[] values)
	{
		var tasks = values.Select(keyValue => GrainFactory
			.GetGrain<IRobotGrain>(keyValue.Id)
			.AddInstruction(keyValue.Instruction)
		);

		return Task.WhenAll(tasks);
	}
}
