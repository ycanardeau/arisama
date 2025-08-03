using System.Text.Json.Serialization;
using Aigamo.Arisama;

namespace Json;

internal interface IMembershipTransition : ITransition;

internal interface ICanSuspend : IMembershipTransition;

internal interface ICanTerminate : IMembershipTransition;

internal interface ICanReactivate : IMembershipTransition;

internal abstract record MembershipCommand : ICommand;

internal sealed record Suspend : MembershipCommand, ICommand<ICanSuspend, Inactive>
{
	Inactive ICommand<ICanSuspend, Inactive>.Execute(ICanSuspend from)
	{
		return new Inactive();
	}
}

internal sealed record Terminate : MembershipCommand, ICommand<ICanTerminate, Terminated>
{
	Terminated ICommand<ICanTerminate, Terminated>.Execute(ICanTerminate from)
	{
		return new Terminated();
	}
}

internal sealed record Reactivate : MembershipCommand, ICommand<ICanReactivate, Active>
{
	Active ICommand<ICanReactivate, Active>.Execute(ICanReactivate from)
	{
		return new Active();
	}
}

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(Inactive), typeDiscriminator: "Inactive")]
[JsonDerivedType(typeof(Active), typeDiscriminator: "Active")]
[JsonDerivedType(typeof(Terminated), typeDiscriminator: "Terminated")]
internal abstract record MembershipState : IState;

internal sealed record Inactive : MembershipState,
	ICanReactivate,
	ICanTerminate;

internal sealed record Active : MembershipState,
	ICanSuspend,
	ICanTerminate;

internal sealed record Terminated : MembershipState,
	ICanReactivate;
