using Microsoft.Extensions.Logging;
using Orleans.Streams;
using WebApp.UrlShortener.Infrastructure.GrainInterfaces;

namespace WebApp.UrlShortener.Infrastructure.GrainClasses;

internal class RobotGrain : Grain, IRobotGrain
{
	private readonly ILogger<RobotGrain> logger;
	private readonly IPersistentState<RobotState> state;
	private readonly string key;
	private readonly IAsyncStream<InstructionMessage> stream;

	public RobotGrain(
		ILogger<RobotGrain> logger,
		[PersistentState(stateName: "robotState", storageName: "robotStateStore")]
		IPersistentState<RobotState> state
	)
	{
		this.logger = logger;
		this.state = state;
		key = this.GetPrimaryKeyString();
		stream = this
			.GetStreamProvider("SMSProvider")
			.GetStream<InstructionMessage>("StartingInstruction", Guid.Empty);
	}

	Task Publish(string instruction)
	{
		var message = new InstructionMessage(instruction, key);

		return stream.OnNextAsync(message);
	}

	public async Task AddInstruction(string instruction)
	{
		var key = this.GetPrimaryKeyString();

		logger.LogWarning("{Key} adding '{Instruction}'", key, instruction);

		state.State.Instructions.Enqueue(instruction);
		await state.WriteStateAsync();
	}

	public Task<int> GetInstructionCount()
	{
		return Task.FromResult(state.State.Instructions.Count);
	}

	public async Task<string?> GetNextInstruction()
	{
		if (state.State.Instructions.Count == 0)
		{
			return null;
		}

		var instruction = state.State.Instructions.Dequeue();
		var key = this.GetPrimaryKeyString();

		logger.LogWarning("{Key} returning '{Instruction}'", key, instruction);

		await Publish(instruction);

		await state.WriteStateAsync();
		return instruction;
	}
}
