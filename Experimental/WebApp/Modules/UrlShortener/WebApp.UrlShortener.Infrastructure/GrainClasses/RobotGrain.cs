using Microsoft.Extensions.Logging;
using WebApp.UrlShortener.Infrastructure.GrainInterfaces;

namespace WebApp.UrlShortener.Infrastructure.GrainClasses;

internal class RobotGrain(ILogger<RobotGrain> logger) : Grain, IRobotGrain
{
	private readonly Queue<string> _instructions = new();

	public Task AddInstruction(string instruction)
	{
		var key = this.GetPrimaryKeyString();
		logger.LogWarning("{Key} adding '{Instruction}'", key, instruction);
		_instructions.Enqueue(instruction);
		return Task.CompletedTask;
	}

	public Task<int> GetInstructionCount()
	{
		return Task.FromResult(_instructions.Count);
	}

	public Task<string?> GetNextInstruction()
	{
		if (_instructions.Count == 0)
		{
			return Task.FromResult<string?>(null);
		}

		var instruction = _instructions.Dequeue();
		var key = this.GetPrimaryKeyString();
		logger.LogWarning("{Key} adding '{Instruction}'", key, instruction);
		return Task.FromResult<string?>(instruction);
	}
}
