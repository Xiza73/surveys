using Microsoft.AspNetCore.Mvc;
using manuel_fajardo_prueba_tecnica.Services.interfaces;
using manuel_fajardo_prueba_tecnica.Models;

namespace manuel_fajardo_prueba_tecnica.Controllers;

[ApiController]
[Route("[controller]")]
public class SurveyController : Controller
{
  private readonly ISurveyService _service;

  public SurveyController(ISurveyService service)
  {
    _service = service;
  }

  [HttpPost]
  public ResponseBase CreateSurvey([FromBody] SurveyRequest request)
  {
    try
    {
      return _service.CreateSurvey(request);
    }
    catch (System.Exception)
    {
      throw;
    }
  }

  [HttpGet]
  public IEnumerable<Survey> GetSurveys()
  {
    try
    {
      return _service.GetSurveys();
    }
    catch (System.Exception)
    {
      throw;
    }
  }

  [HttpGet]
  [Route("GetQuestions")]
  public IEnumerable<Question> GetQuestions()
  {
    try
    {
      return _service.GetQuestions();
    }
    catch (System.Exception)
    {
      throw;
    }
  }

  [HttpGet]
  [Route("GetSurveysByBranch/{id}")]
  public IEnumerable<SurveyByBranch> GetSurveysByBranch(int id)
  {
    try
    {
      return _service.GetSurveysByBranch(id);
    }
    catch (System.Exception)
    {
      throw;
    }
  }
}
