using Aigamo.Arisama;
using Microsoft.Extensions.Logging;

namespace MaritalStateMachine;

static class Program
{
	static async Task Main()
	{
		var loggerFactory = LoggerFactory.Create(builder =>
		{
			builder.AddSimpleConsole(options =>
			{
				options.IncludeScopes = true;
				options.SingleLine = true;
				options.TimestampFormat = "HH:mm:ss ";
			});
		});

		var maritalStateMachine = new StateMachineBuilder<IMaritalTransition, MaritalCommand, MaritalStatus>(loggerFactory)
			.AddTransition<ICanMarry, Marry, Married>()
			.AddTransition<ICanDivorce, Divorce, Divorced>()
			.AddTransition<ICanBecomeWidowed, BecomeWidowed, Widowed>()
			.AddTransition<ICanDecease, Decease, Deceased>()
			.Build([new Single()]);

		await maritalStateMachine.SendAsync(new Marry());
		await maritalStateMachine.SendAsync(new Divorce());
		await maritalStateMachine.SendAsync(new Marry());
		await maritalStateMachine.SendAsync(new BecomeWidowed());
		await maritalStateMachine.SendAsync(new Marry());
		await maritalStateMachine.SendAsync(new Decease());

		foreach (var state in maritalStateMachine.States)
		{
			Console.WriteLine(state);
		}
	}
}
