using Aigamo.Arisama;

namespace Alarm;

internal abstract record AlarmCommand : ICommand;

internal sealed record Startup : AlarmCommand, ICommand<ICanStartup, Disarmed>
{
	public Disarmed Execute(ICanStartup from)
	{
		return new Disarmed();
	}
}

internal sealed record Arm : AlarmCommand, ICommand<ICanArm, PreArmed>
{
	public PreArmed Execute(ICanArm from)
	{
		return new PreArmed();
	}
}

internal sealed record Disarm : AlarmCommand, ICommand<ICanDisarm, Disarmed>
{
	public Disarmed Execute(ICanDisarm from)
	{
		return new Disarmed();
	}
}

internal sealed record Trigger : AlarmCommand, ICommand<ICanTrigger, PreTriggered>
{
	public PreTriggered Execute(ICanTrigger from)
	{
		return new PreTriggered();
	}
}

internal sealed record Acknowledge : AlarmCommand, ICommand<ICanAcknowledge, Acknowledged>
{
	public Acknowledged Execute(ICanAcknowledge from)
	{
		return new Acknowledged();
	}
}

internal sealed record Pause : AlarmCommand, ICommand<ICanPause, ArmPaused>
{
	public ArmPaused Execute(ICanPause from)
	{
		return new ArmPaused();
	}
}

internal sealed record TimeOutArmed : AlarmCommand, ICommand<ICanTimeOutArmed, Armed>
{
	public Armed Execute(ICanTimeOutArmed from)
	{
		return new Armed();
	}
}

internal sealed record TimeOutTriggered : AlarmCommand, ICommand<ICanTimeOutTriggered, Triggered>
{
	public Triggered Execute(ICanTimeOutTriggered from)
	{
		return new Triggered();
	}
}
