using Microsoft.Extensions.Logging;
using Orleans.Transactions.Abstractions;
using WebApp.UrlShortener.Infrastructure.GrainInterfaces;

namespace WebApp.UrlShortener.Infrastructure.GrainClasses;

internal class RobotGrain(
	ILogger<RobotGrain> logger,
	[TransactionalState(stateName: "robotState", storageName: "robotStateStore")]
	ITransactionalState<RobotState> state
) : Grain, IRobotGrain
{
	public async Task AddInstruction(string instruction)
	{
		var key = this.GetPrimaryKeyString();

		logger.LogWarning("{Key} adding '{Instruction}'", key, instruction);

		await state.PerformUpdate(state => state.Instructions.Enqueue(instruction));
	}

	public Task<int> GetInstructionCount()
	{
		return state.PerformRead(state => state.Instructions.Count);
	}

	public async Task<string?> GetNextInstruction()
	{
		var key = this.GetPrimaryKeyString();

		var instruction = await state.PerformUpdate(state =>
		{
			if (state.Instructions.Count == 0)
			{
				return null;
			}

			return state.Instructions.Dequeue();
		});

		if (instruction is not null)
		{
			logger.LogWarning("{Key} returning '{Instruction}'", key, instruction);
		}

		return instruction;
	}
}
