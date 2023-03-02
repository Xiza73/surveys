using Microsoft.AspNetCore.Mvc;
using manuel_fajardo_prueba_tecnica.Services.interfaces;
using manuel_fajardo_prueba_tecnica.Models;

namespace manuel_fajardo_prueba_tecnica.Controllers;


[ApiController]
[Route("[controller]")]
public class PersonController : Controller
{
  private readonly IPersonService _service;
  public PersonController(IPersonService service)
  {
    _service = service;
  }

  [HttpGet]
  public IEnumerable<Person> GetPeople()
  {
    try
    {
      return _service.GetPeople();
    }
    catch (System.Exception)
    {
      throw;
    }
  }

  [HttpGet]
  [Route("GetPerson")]
  public Person GetPerson(int id)
  {
    try
    {
      return _service.GetPerson(id);
    }
    catch (System.Exception)
    {
      throw;
    }
  }

  [HttpPost]
  public Person CreatePerson([FromBody] Person person)
  {
    try
    {
      return _service.CreatePerson(person);
    }
    catch (System.Exception)
    {
      throw;
    }
  }
}
