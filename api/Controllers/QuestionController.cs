using Microsoft.AspNetCore.Mvc;
using manuel_fajardo_prueba_tecnica.Services.interfaces;
using manuel_fajardo_prueba_tecnica.Models;

namespace manuel_fajardo_prueba_tecnica.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionController : Controller
{
  private readonly IQuestionService _service;
  public QuestionController(IQuestionService service)
  {
    _service = service;
  }

  [HttpGet]
  public IEnumerable<Question> GetPeople()
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
}
