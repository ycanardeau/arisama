using WebApp.CivilRegistration.Contracts.Persons.Dtos;
using WebApp.CivilRegistration.Domain.Persons.Entities;

namespace WebApp.CivilRegistration.Application.Interfaces.Mappers;

internal interface IPersonMapper
{
	PersonDto Map(Person person);
}
