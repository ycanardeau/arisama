using Aigamo.Arisama;

namespace Alarm;

internal interface IAlarmTransition : ITransition;

internal interface ICanStartup : IAlarmTransition;

internal interface ICanArm : IAlarmTransition;

internal interface ICanDisarm : IAlarmTransition;

internal interface ICanTrigger : IAlarmTransition;

internal interface ICanAcknowledge : IAlarmTransition;

internal interface ICanPause : IAlarmTransition;

internal interface ICanTimeOutArmed : IAlarmTransition;

internal interface ICanTimeOutTriggered : IAlarmTransition;
