using Aigamo.Arisama;
using Microsoft.Extensions.Logging;

namespace Alarm;

static class Program
{
	static void Main()
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

		var alarm = new StateMachineBuilder<IAlarmTransition, AlarmCommand, AlarmState>(loggerFactory)
			.AddTransition<ICanStartup, Startup, Disarmed>()
			.AddTransition<ICanArm, Arm, PreArmed>()
			.AddTransition<ICanDisarm, Disarm, Disarmed>()
			.AddTransition<ICanTrigger, Trigger, PreTriggered>()
			.AddTransition<ICanAcknowledge, Acknowledge, Acknowledged>()
			.AddTransition<ICanPause, Pause, ArmPaused>()
			.AddTransition<ICanTimeOutArmed, TimeOutArmed, Armed>()
			.AddTransition<ICanTimeOutTriggered, TimeOutTriggered, Triggered>()
			.Build([new Undefined()]);
	}
}
