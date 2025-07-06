using Nut.Results;
using WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains;

internal class MaritalStateMachineGrain(
	[PersistentState(stateName: "maritalStateMachine", storageName: "maritalStateMachines")]
	IPersistentState<MaritalStateMachineState> state
) : Grain, IMaritalStateMachineGrain
{
	private MaritalStatus? CurrentState => state.State.States.MaxBy(x => x.Version);

	private void AddState(MaritalStatus maritalStatus)
	{
		state.State.States.Add(maritalStatus);
	}

	public Task<Result> Initialize()
	{
		if (CurrentState is not null)
		{
			return Result.Error(new InvalidOperationException()).AsTask();
		}

		AddState(new Single
		{
			Version = 1,
		});

		return Result.Ok().AsTask();
	}

	public Task<Result> Marry(Guid marryWith)
	{
		if (CurrentState is not ICanMarry currentState)
		{
			return Result.Error(new InvalidOperationException()).AsTask();
		}

		AddState(new Married
		{
			Version = currentState.Version + 1,
			MarryWith = marryWith,
		});

		return Result.Ok().AsTask();
	}

	public Task<Result> Divorce()
	{
		if (CurrentState is not ICanDivorce currentState)
		{
			return Result.Error(new InvalidOperationException()).AsTask();
		}

		AddState(new Divorced
		{
			Version = currentState.Version + 1,
		});

		return Result.Ok().AsTask();
	}

	public Task<Result> BecomeWidowed()
	{
		if (CurrentState is not ICanBecomeWidowed currentState)
		{
			return Result.Error(new InvalidOperationException()).AsTask();
		}

		AddState(new Widowed
		{
			Version = currentState.Version + 1,
		});

		return Result.Ok().AsTask();
	}

	public Task<Result> Decease()
	{
		if (CurrentState is not ICanDecease currentState)
		{
			return Result.Error(new InvalidOperationException()).AsTask();
		}

		AddState(new Deceased
		{
			Version = currentState.Version + 1,
		});

		return Result.Ok().AsTask();
	}
}
