using WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains;

internal class PersonGrain(
	[PersistentState(stateName: "person", storageName: "people")]
		IPersistentState<PersonState> state
) : Grain, IPersonGrain
{
	private IMaritalStateMachineGrain _maritalStateMachineGrain = null!;

	public override Task OnActivateAsync(CancellationToken cancellationToken)
	{
		_maritalStateMachineGrain = GrainFactory.GetGrain<IMaritalStateMachineGrain>(
			this.GetPrimaryKey()
		);
		return base.OnActivateAsync(cancellationToken);
	}

	public Task<Result<Unit, CivilRegistrationOrleansError>> Initialize()
	{
		return _maritalStateMachineGrain.Initialize();
	}

	public Task<Result<Unit, CivilRegistrationOrleansError>> Marry(Guid marryWith)
	{
		return _maritalStateMachineGrain.Marry(marryWith);
	}

	public Task<Result<Unit, CivilRegistrationOrleansError>> Divorce()
	{
		return _maritalStateMachineGrain.Divorce();
	}

	public Task<Result<Unit, CivilRegistrationOrleansError>> BecomeWidowed()
	{
		return _maritalStateMachineGrain.BecomeWidowed();
	}

	public Task<Result<Unit, CivilRegistrationOrleansError>> Decease()
	{
		return _maritalStateMachineGrain.Decease();
	}
}
