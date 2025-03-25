using FastEndpoints;

namespace WebApp.CivilRegistration.Endpoints.Persons;

internal class PersonsGroup : Group
{
	public PersonsGroup()
	{
		Configure("/persons", ep =>
		{
		});
	}
}
