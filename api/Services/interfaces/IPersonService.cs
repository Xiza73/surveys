using manuel_fajardo_prueba_tecnica.Models;

namespace manuel_fajardo_prueba_tecnica.Services.interfaces;

public interface IPersonService
{
  public IEnumerable<Person> GetPeople();
  
  public Person GetPerson(int id);

  public Person CreatePerson(Person person);
}
