using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace WebApp.CivilRegistration.Endpoints.Persons;

internal class PersonsGroup : Group
{
	public PersonsGroup()
	{
		Configure("/persons", ep =>
		{
			ep.Description(builder => builder.WithTags("Persons"));
		});
	}
}
