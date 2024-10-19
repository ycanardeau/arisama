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

static class Program
{
	static void Main()
	{
		var alarm = new StateMachineBuilder<IAlarmState, IAlarmCommand>()
			.AddCommand<Startup>()
			.AddCommand<Arm>()
			.AddCommand<Disarm>()
			.AddCommand<Trigger>()
			.AddCommand<Acknowledge>()
			.AddCommand<Pause>()
			.AddCommand<TimeOutArmed>()
			.AddCommand<TimeOutTriggered>()
			.Build(new Undefined());
	}
}
