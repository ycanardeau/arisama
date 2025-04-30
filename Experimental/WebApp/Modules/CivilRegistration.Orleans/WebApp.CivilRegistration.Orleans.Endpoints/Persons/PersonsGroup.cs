using FastEndpoints;

namespace WebApp.CivilRegistration.Orleans.Endpoints.Persons;

internal class PersonsGroup : Group
{
	public PersonsGroup()
	{
		Configure("/orleans.persons", ep =>
		{
		});
	}
}
