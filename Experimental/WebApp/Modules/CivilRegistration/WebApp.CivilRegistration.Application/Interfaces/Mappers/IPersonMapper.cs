using WebApp.CivilRegistration.Contracts.People.Dtos;
using WebApp.CivilRegistration.Domain.People.Entities;

namespace WebApp.CivilRegistration.Application.Interfaces.Mappers;

internal interface IPersonMapper
{
	PersonDto Map(Person person);
}
