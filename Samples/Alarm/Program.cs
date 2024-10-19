using Aigamo.Arisama;
using static IAlarmCommand;
using static IAlarmState;

partial interface IAlarmState : IState
{
	public interface ICanStartup : IAlarmState;

	public interface ICanArm : IAlarmState;

	public interface ICanDisarm : IAlarmState;

	public interface ICanTrigger : IAlarmState;

	public interface ICanAcknowledge : IAlarmState;

	public interface ICanPause : IAlarmState;

	public interface ICanTimeOutArmed : IAlarmState;

	public interface ICanTimeOutTriggered : IAlarmState;
}

partial interface IAlarmState : IState
{
	public sealed record Undefined : IAlarmState,
		ICanStartup;

	public sealed record Disarmed : IAlarmState,
		ICanArm;

	public sealed record PreArmed : IAlarmState,
		ICanDisarm,
		ICanTimeOutArmed;

	public sealed record Armed : IAlarmState,
		ICanDisarm,
		ICanTrigger,
		ICanPause;

	public sealed record Triggered : IAlarmState,
		ICanAcknowledge,
		ICanTimeOutArmed;

	public sealed record ArmPaused : IAlarmState,
		ICanTrigger,
		ICanTimeOutArmed;

	public sealed record PreTriggered : IAlarmState,
		ICanDisarm,
		ICanTimeOutTriggered;

	public sealed record Acknowledged : IAlarmState,
		ICanDisarm;
}

interface IAlarmCommand : ICommand
{
	public sealed record Startup : IAlarmCommand;

	public sealed record Arm : IAlarmCommand;

	public sealed record Disarm : IAlarmCommand;

	public sealed record Trigger : IAlarmCommand;

	public sealed record Acknowledge : IAlarmCommand;

	public sealed record Pause : IAlarmCommand;

	public sealed record TimeOutArmed : IAlarmCommand;

	public sealed record TimeOutTriggered : IAlarmCommand;
}

static class Program
{
	static void Main()
	{
		var alarm = new StateMachineBuilder<IAlarmState, IAlarmCommand>()
			.ConfigureState<ICanStartup, Startup, Disarmed>((from, command) => new Disarmed())
			.ConfigureState<ICanArm, Arm, PreArmed>((from, command) => new PreArmed())
			.ConfigureState<ICanDisarm, Disarm, Disarmed>((from, command) => new Disarmed())
			.ConfigureState<ICanTrigger, Trigger, PreTriggered>((from, command) => new PreTriggered())
			.ConfigureState<ICanAcknowledge, Acknowledge, Acknowledged>((from, command) => new Acknowledged())
			.ConfigureState<ICanPause, Pause, ArmPaused>((from, command) => new ArmPaused())
			.ConfigureState<ICanTimeOutArmed, TimeOutArmed, Armed>((from, command) => new Armed())
			.ConfigureState<ICanTimeOutTriggered, TimeOutTriggered, Triggered>((from, command) => new Triggered())
			.Build(new Undefined());
	}
}
