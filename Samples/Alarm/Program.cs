using Aigamo.Arisama;
using static IAlarmCommand;
using static IAlarmState;
using static IAlarmTransition;

interface IAlarmTransition : ITransition
{
	public interface ICanStartup : IAlarmTransition;

	public interface ICanArm : IAlarmTransition;

	public interface ICanDisarm : IAlarmTransition;

	public interface ICanTrigger : IAlarmTransition;

	public interface ICanAcknowledge : IAlarmTransition;

	public interface ICanPause : IAlarmTransition;

	public interface ICanTimeOutArmed : IAlarmTransition;

	public interface ICanTimeOutTriggered : IAlarmTransition;
}

interface IAlarmCommand : ICommand
{
	public sealed record Startup : IAlarmCommand, ICommand<ICanStartup>;

	public sealed record Arm : IAlarmCommand, ICommand<ICanArm>;

	public sealed record Disarm : IAlarmCommand, ICommand<ICanDisarm>;

	public sealed record Trigger : IAlarmCommand, ICommand<ICanTrigger>;

	public sealed record Acknowledge : IAlarmCommand, ICommand<ICanAcknowledge>;

	public sealed record Pause : IAlarmCommand, ICommand<ICanPause>;

	public sealed record TimeOutArmed : IAlarmCommand, ICommand<ICanTimeOutArmed>;

	public sealed record TimeOutTriggered : IAlarmCommand, ICommand<ICanTimeOutTriggered>;
}

interface IAlarmState : IState
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

static class Program
{
	static void Main()
	{
		var alarm = new StateMachineBuilder<IAlarmTransition, IAlarmCommand, IAlarmState>()
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
