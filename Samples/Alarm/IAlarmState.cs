using Aigamo.Arisama;
using static Alarm.IAlarmTransition;

namespace Alarm;

interface IAlarmState : IState
{
	public sealed record Undefined : IAlarmState
		, ICanStartup;

	public sealed record Disarmed : IAlarmState
		, ICanArm;

	public sealed record PreArmed : IAlarmState
		, ICanDisarm
		, ICanTimeOutArmed;

	public sealed record Armed : IAlarmState
		, ICanDisarm
		, ICanTrigger
		, ICanPause;

	public sealed record Triggered : IAlarmState
		, ICanAcknowledge
		, ICanTimeOutArmed;

	public sealed record ArmPaused : IAlarmState
		, ICanTrigger
		, ICanTimeOutArmed;

	public sealed record PreTriggered : IAlarmState
		, ICanDisarm
		, ICanTimeOutTriggered;

	public sealed record Acknowledged : IAlarmState
		, ICanDisarm;
}
