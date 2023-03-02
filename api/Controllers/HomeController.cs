using Microsoft.AspNetCore.Mvc;

namespace manuel_fajardo_prueba_tecnica.Controllers;

[ApiController]
[Route("")]
public class HomeController : Controller
{
  public HomeController()
  {
  }

  [HttpGet]
  public string Index()
  {
    return "Hello World!";
  }
}
