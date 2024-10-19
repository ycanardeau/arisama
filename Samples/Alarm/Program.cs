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
			.From<ICanStartup>()
				.On<Startup>()
				.To((from, command) => new Disarmed())
			.From<ICanArm>()
				.On<Arm>()
				.To((from, command) => new PreArmed())
			.From<ICanDisarm>()
				.On<Disarm>()
				.To((from, command) => new Disarmed())
			.From<ICanTrigger>()
				.On<Trigger>()
				.To((from, command) => new PreTriggered())
			.From<ICanAcknowledge>()
				.On<Acknowledge>()
				.To((from, command) => new Acknowledged())
			.From<ICanPause>()
				.On<Pause>()
				.To((from, command) => new ArmPaused())
			.From<ICanTimeOutArmed>()
				.On<TimeOutArmed>()
				.To((from, command) => new Armed())
			.From<ICanTimeOutTriggered>()
				.On<TimeOutTriggered>()
				.To((from, command) => new Triggered())
			.Build(new Undefined());
	}
}
