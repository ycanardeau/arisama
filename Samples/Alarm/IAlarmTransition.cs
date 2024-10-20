using Aigamo.Arisama;

namespace Alarm;

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
