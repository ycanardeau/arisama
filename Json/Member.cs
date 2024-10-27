using System.Text.Json.Serialization;
using Aigamo.Arisama;
using static Json.IMembershipState;
using static Json.IMembershipTransition;

namespace Json;

interface IMembershipTransition : ITransition
{
	public interface ICanSuspend : IMembershipTransition;

	public interface ICanTerminate : IMembershipTransition;

	public interface ICanReactivate : IMembershipTransition;
}

interface IMembershipCommand : ICommand
{
	public sealed record Suspend : IMembershipCommand, ICommand<ICanSuspend, Inactive>
	{
		Inactive ICommand<ICanSuspend, Inactive>.Execute(ICanSuspend from)
		{
			return new Inactive();
		}
	}

	public sealed record Terminate : IMembershipCommand, ICommand<ICanTerminate, Terminated>
	{
		Terminated ICommand<ICanTerminate, Terminated>.Execute(ICanTerminate from)
		{
			return new Terminated();
		}
	}

	public sealed record Reactivate : IMembershipCommand, ICommand<ICanReactivate, Active>
	{
		Active ICommand<ICanReactivate, Active>.Execute(ICanReactivate from)
		{
			return new Active();
		}
	}
}

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(Inactive), typeDiscriminator: "Inactive")]
[JsonDerivedType(typeof(Active), typeDiscriminator: "Active")]
[JsonDerivedType(typeof(Terminated), typeDiscriminator: "Terminated")]
interface IMembershipState : IState
{
	public sealed record Inactive : IMembershipState,
		ICanReactivate,
		ICanTerminate;

	public sealed record Active : IMembershipState,
		ICanSuspend,
		ICanTerminate;

	public sealed record Terminated : IMembershipState,
		ICanReactivate;
}
