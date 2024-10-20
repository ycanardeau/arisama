using Aigamo.Arisama;
using static Alarm.IAlarmState;
using static Alarm.IAlarmTransition;

namespace Alarm;

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
