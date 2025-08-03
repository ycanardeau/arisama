using Aigamo.Arisama;

namespace MaritalStateMachine;

internal abstract record MaritalCommand : ICommand;

internal sealed record Marry : MaritalCommand, ICommand<ICanMarry, Married>
{
	public Married Execute(ICanMarry from)
	{
		return new Married();
	}
}

internal sealed record Divorce : MaritalCommand, ICommand<ICanDivorce, Divorced>
{
	public Divorced Execute(ICanDivorce from)
	{
		return new Divorced();
	}
}

internal sealed record BecomeWidowed : MaritalCommand, ICommand<ICanBecomeWidowed, Widowed>
{
	public Widowed Execute(ICanBecomeWidowed from)
	{
		return new Widowed();
	}
}

internal sealed record Decease : MaritalCommand, ICommand<ICanDecease, Deceased>
{
	public Deceased Execute(ICanDecease from)
	{
		return new Deceased();
	}
}
