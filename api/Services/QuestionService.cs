using manuel_fajardo_prueba_tecnica.Services.interfaces;
using manuel_fajardo_prueba_tecnica.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace manuel_fajardo_prueba_tecnica.Services;

public class QuestionService : IQuestionService
{
  private readonly DbRetailContext _context;

  public QuestionService(DbRetailContext context)
  {
    _context = context;
  }

  public IEnumerable<Question> GetQuestions()
  {
    var result = _context.Database.ExecuteSqlRaw("EXEC sp_get_questions");
    Console.WriteLine(result);
    return _context.Questions?.ToList() ?? new List<Question>();
  }
}
