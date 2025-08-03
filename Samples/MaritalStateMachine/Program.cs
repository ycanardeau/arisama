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
			.ConfigureState<ICanMarry, Marry, Married>()
			.ConfigureState<ICanDivorce, Divorce, Divorced>()
			.ConfigureState<ICanBecomeWidowed, BecomeWidowed, Widowed>()
			.ConfigureState<ICanDecease, Decease, Deceased>()
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
