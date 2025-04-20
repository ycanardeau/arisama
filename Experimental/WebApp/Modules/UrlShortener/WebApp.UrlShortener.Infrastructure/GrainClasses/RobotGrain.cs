using Microsoft.Extensions.Logging;
using WebApp.UrlShortener.Infrastructure.GrainInterfaces;

namespace WebApp.UrlShortener.Infrastructure.GrainClasses;

internal class RobotGrain(
	ILogger<RobotGrain> logger,
	[PersistentState(stateName: "robotState", storageName: "robotStateStore")]
	IPersistentState<RobotState> state
) : Grain, IRobotGrain
{
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

		logger.LogWarning("{Key} adding '{Instruction}'", key, instruction);

		await state.WriteStateAsync();
		return instruction;
	}
}
