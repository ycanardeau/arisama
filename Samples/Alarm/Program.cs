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
	public sealed record Startup : IAlarmCommand, ICommand<ICanStartup, Disarmed>
	{
		public Disarmed Execute(ICanStartup from)
		{
			return new Disarmed();
		}
	}

	public sealed record Arm : IAlarmCommand, ICommand<ICanArm, PreArmed>
	{
		public PreArmed Execute(ICanArm from)
		{
			return new PreArmed();
		}
	}

	public sealed record Disarm : IAlarmCommand, ICommand<ICanDisarm, Disarmed>
	{
		public Disarmed Execute(ICanDisarm from)
		{
			return new Disarmed();
		}
	}

	public sealed record Trigger : IAlarmCommand, ICommand<ICanTrigger, PreTriggered>
	{
		public PreTriggered Execute(ICanTrigger from)
		{
			return new PreTriggered();
		}
	}

	public sealed record Acknowledge : IAlarmCommand, ICommand<ICanAcknowledge, Acknowledged>
	{
		public Acknowledged Execute(ICanAcknowledge from)
		{
			return new Acknowledged();
		}
	}

	public sealed record Pause : IAlarmCommand, ICommand<ICanPause, ArmPaused>
	{
		public ArmPaused Execute(ICanPause from)
		{
			return new ArmPaused();
		}
	}

	public sealed record TimeOutArmed : IAlarmCommand, ICommand<ICanTimeOutArmed, Armed>
	{
		public Armed Execute(ICanTimeOutArmed from)
		{
			return new Armed();
		}
	}

	public sealed record TimeOutTriggered : IAlarmCommand, ICommand<ICanTimeOutTriggered, Triggered>
	{
		public Triggered Execute(ICanTimeOutTriggered from)
		{
			return new Triggered();
		}
	}
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
			.ConfigureState<ICanStartup, Startup, Disarmed>()
			.ConfigureState<ICanArm, Arm, PreArmed>()
			.ConfigureState<ICanDisarm, Disarm, Disarmed>()
			.ConfigureState<ICanTrigger, Trigger, PreTriggered>()
			.ConfigureState<ICanAcknowledge, Acknowledge, Acknowledged>()
			.ConfigureState<ICanPause, Pause, ArmPaused>()
			.ConfigureState<ICanTimeOutArmed, TimeOutArmed, Armed>()
			.ConfigureState<ICanTimeOutTriggered, TimeOutTriggered, Triggered>()
			.Build(new Undefined());
	}
}
