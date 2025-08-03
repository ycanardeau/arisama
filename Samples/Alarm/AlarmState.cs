using Aigamo.Arisama;

namespace Alarm;

public abstract record AlarmState : IState;

public sealed record Undefined : AlarmState
	, ICanStartup;

public sealed record Disarmed : AlarmState
	, ICanArm;

public sealed record PreArmed : AlarmState
	, ICanDisarm
	, ICanTimeOutArmed;

public sealed record Armed : AlarmState
	, ICanDisarm
	, ICanTrigger
	, ICanPause;

public sealed record Triggered : AlarmState
	, ICanAcknowledge
	, ICanTimeOutArmed;

public sealed record ArmPaused : AlarmState
	, ICanTrigger
	, ICanTimeOutArmed;

public sealed record PreTriggered : AlarmState
	, ICanDisarm
	, ICanTimeOutTriggered;

public sealed record Acknowledged : AlarmState
	, ICanDisarm;
