using Aigamo.Arisama;
using static Alarm.IAlarmCommand;
using static Alarm.IAlarmState;
using static Alarm.IAlarmTransition;

namespace Alarm;

static class Program
{
	static void Main()
	{
		var alarm = new StateMachineBuilder<IAlarmTransition, IAlarmCommand, IAlarmState>()
			.ConfigureState<ICanStartup, Startup, Disarmed>()
			.ConfigureState<ICanArm, Arm, PreArmed>()
			.ConfigureState<ICanDisarm, Disarm, Disarmed>()
			.ConfigureState<ICanTrigger, Trigger, PreTriggered>()
			.ConfigureState<ICanAcknowledge, Acknowledge, Acknowledged>()
			.ConfigureState<ICanPause, Pause, ArmPaused>()
			.ConfigureState<ICanTimeOutArmed, TimeOutArmed, Armed>()
			.ConfigureState<ICanTimeOutTriggered, TimeOutTriggered, Triggered>()
			.Build([new Undefined()]);
	}
}
