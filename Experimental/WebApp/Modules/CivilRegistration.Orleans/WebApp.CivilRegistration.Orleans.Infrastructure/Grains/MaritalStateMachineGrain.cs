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

	public Task<Result<Unit, WebAppError>> Initialize()
	{
		if (CurrentState is not null)
		{
			return UnprocessableEntity().AsTask();
		}

		AddState(new Single { Version = 1 });

		return Ok().AsTask();
	}

	public Task<Result<Unit, WebAppError>> Marry(Guid marryWith)
	{
		if (CurrentState is not ICanMarry currentState)
		{
			return UnprocessableEntity().AsTask();
		}

		AddState(new Married { Version = currentState.Version + 1, MarryWith = marryWith });

		return Ok().AsTask();
	}

	public Task<Result<Unit, WebAppError>> Divorce()
	{
		if (CurrentState is not ICanDivorce currentState)
		{
			return UnprocessableEntity().AsTask();
		}

		AddState(new Divorced { Version = currentState.Version + 1 });

		return Ok().AsTask();
	}

	public Task<Result<Unit, WebAppError>> BecomeWidowed()
	{
		if (CurrentState is not ICanBecomeWidowed currentState)
		{
			return UnprocessableEntity().AsTask();
		}

		AddState(new Widowed { Version = currentState.Version + 1 });

		return Ok().AsTask();
	}

	public Task<Result<Unit, WebAppError>> Decease()
	{
		if (CurrentState is not ICanDecease currentState)
		{
			return UnprocessableEntity().AsTask();
		}

		AddState(new Deceased { Version = currentState.Version + 1 });

		return Ok().AsTask();
	}
}
