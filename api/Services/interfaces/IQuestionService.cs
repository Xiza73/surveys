using manuel_fajardo_prueba_tecnica.Models;

namespace manuel_fajardo_prueba_tecnica.Services.interfaces;

public interface IQuestionService
{
  public IEnumerable<Question> GetQuestions();
}
