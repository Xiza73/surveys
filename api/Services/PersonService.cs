using manuel_fajardo_prueba_tecnica.Services.interfaces;
using manuel_fajardo_prueba_tecnica.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace manuel_fajardo_prueba_tecnica.Services;

public class PersonService : IPersonService
{
  private readonly DbRetailContext _context;

  public PersonService(DbRetailContext context)
  {
    _context = context;
  }

  public IEnumerable<Person> GetPeople()
  {
    return _context.People?.ToList() ?? new List<Person>();
  }

  public Person GetPerson(int id)
  {
    return _context.People?.FirstOrDefault(p => p.IdPerson == id) ?? new Person();
  }

  public Person CreatePerson(Person person)
  {
    var parameters = new List<SqlParameter>();
    parameters.Add(new SqlParameter("@name", person.Name));
    parameters.Add(new SqlParameter("@identification", person.Identification));
    parameters.Add(new SqlParameter("@sex", person.Sex));
    parameters.Add(new SqlParameter("@age", person.Age));

    var result = _context.Database.ExecuteSqlRaw("EXEC sp_create_person @name, @identification, @sex, @age", parameters);
    return person;
  }
}
