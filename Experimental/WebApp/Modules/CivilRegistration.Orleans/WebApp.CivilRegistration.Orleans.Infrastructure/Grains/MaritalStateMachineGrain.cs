using WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains;

internal class MaritalStateMachineGrain(
	[PersistentState(stateName: "maritalStateMachine", storageName: "maritalStateMachines")]
	IPersistentState<MaritalStateMachineState> state
) : Grain, IMaritalStateMachineGrain
{
	public Task Initialize()
	{
		if (state.State.States.Count != 0)
		{
			throw new InvalidOperationException();
		}

		state.State.States.Add(new Single
		{
			Version = 1,
		});

		return Task.CompletedTask;
	}

	public Task Marry()
	{
		if (state.State.States.MaxBy(x => x.Version) is not ICanMarry currentState)
		{
			throw new InvalidOperationException();
		}

		state.State.States.Add(new Married
		{
			Version = currentState.Version + 1,
		});

		return Task.CompletedTask;
	}

	public Task Divorce()
	{
		if (state.State.States.MaxBy(x => x.Version) is not ICanDivorce currentState)
		{
			throw new InvalidOperationException();
		}

		state.State.States.Add(new Divorced
		{
			Version = currentState.Version + 1,
		});

		return Task.CompletedTask;
	}

	public Task BecomeWidowed()
	{
		if (state.State.States.MaxBy(x => x.Version) is not ICanBecomeWidowed currentState)
		{
			throw new InvalidOperationException();
		}

		state.State.States.Add(new Widowed
		{
			Version = currentState.Version + 1,
		});

		return Task.CompletedTask;
	}

	public Task Decease()
	{
		if (state.State.States.MaxBy(x => x.Version) is not ICanDecease currentState)
		{
			throw new InvalidOperationException();
		}

		state.State.States.Add(new Deceased
		{
			Version = currentState.Version + 1,
		});

		return Task.CompletedTask;
	}
}
