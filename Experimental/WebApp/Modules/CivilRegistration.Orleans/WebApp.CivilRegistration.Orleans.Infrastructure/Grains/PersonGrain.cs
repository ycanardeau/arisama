using WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains;

internal class PersonGrain(
	[PersistentState(stateName: "person", storageName: "persons")]
	IPersistentState<PersonState> state
) : Grain, IPersonGrain
{
	public Task Initialize()
	{
		var maritalStateMachineGrain = GrainFactory.GetGrain<IMaritalStateMachineGrain>(this.GetPrimaryKeyString());

		return maritalStateMachineGrain.Initialize();
	}

	public Task Marry()
	{
		var maritalStateMachineGrain = GrainFactory.GetGrain<IMaritalStateMachineGrain>(this.GetPrimaryKeyString());

		return maritalStateMachineGrain.Marry();
	}

	public Task Divorce()
	{
		var maritalStateMachineGrain = GrainFactory.GetGrain<IMaritalStateMachineGrain>(this.GetPrimaryKeyString());

		return maritalStateMachineGrain.Divorce();
	}

	public Task BecomeWidowed()
	{
		var maritalStateMachineGrain = GrainFactory.GetGrain<IMaritalStateMachineGrain>(this.GetPrimaryKeyString());

		return maritalStateMachineGrain.BecomeWidowed();
	}

	public Task Decease()
	{
		var maritalStateMachineGrain = GrainFactory.GetGrain<IMaritalStateMachineGrain>(this.GetPrimaryKeyString());

		return maritalStateMachineGrain.Decease();
	}
}
