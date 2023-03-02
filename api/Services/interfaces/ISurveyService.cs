using manuel_fajardo_prueba_tecnica.Models;

namespace manuel_fajardo_prueba_tecnica.Services.interfaces;

public interface ISurveyService
{
  public ResponseBase CreateSurvey(SurveyRequest request);

  public IEnumerable<Survey> GetSurveys();

  public IEnumerable<Question> GetQuestions();

  public IEnumerable<SurveyByBranch> GetSurveysByBranch(int id);
}
