using manuel_fajardo_prueba_tecnica.Services.interfaces;
using manuel_fajardo_prueba_tecnica.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace manuel_fajardo_prueba_tecnica.Services;

public class SurveyService : ISurveyService
{
  private readonly DbRetailContext _context;

  public SurveyService(DbRetailContext context)
  {
    _context = context;
  }

  public ResponseBase CreateSurvey(SurveyRequest request)
  {
    var parameters = new List<SqlParameter>();
    parameters.Add(new SqlParameter("@id_branch", request.IdBranch));
    parameters.Add(new SqlParameter("@id_person", request.idPerson));
    parameters.Add(new SqlParameter("@questions", request.Questions));

    var result = _context.Database.ExecuteSqlRaw("EXEC sp_save_survey @id_branch, @id_person, @questions", parameters);
    return new ResponseBase("OK", 200);
  }

  public IEnumerable<Survey> GetSurveys()
  {
    return _context.Surveys?.ToList() ?? new List<Survey>();
  }

  public IEnumerable<Question> GetQuestions()
  {
    return _context.Questions?.ToList() ?? new List<Question>();
  }

  public IEnumerable<SurveyByBranch> GetSurveysByBranch(int id)
  {
    var result = _context.Database.ExecuteSqlRaw("EXEC sp_get_surveys_by_branch @id_branch", new SqlParameter("@id_branch", id));
    var data = _context.SurveyByBranches?.FromSqlRaw("EXEC sp_get_surveys_by_branch @id_branch", new SqlParameter("@id_branch", id)).ToList();
    Console.WriteLine(result);
    Console.WriteLine(data);
    return data ?? new List<SurveyByBranch>();
  }
}
