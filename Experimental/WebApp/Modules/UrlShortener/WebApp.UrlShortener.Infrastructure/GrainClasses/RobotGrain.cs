using WebApp.UrlShortener.Infrastructure.GrainInterfaces;

namespace WebApp.UrlShortener.Infrastructure.GrainClasses;

internal class RobotGrain : Grain, IRobotGrain
{
	private readonly Queue<string> _instructions = new();

	public Task AddInstruction(string instruction)
	{
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
		return Task.FromResult<string?>(instruction);
	}
}
