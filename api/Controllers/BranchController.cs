using Microsoft.AspNetCore.Mvc;
using manuel_fajardo_prueba_tecnica.Services.interfaces;
using manuel_fajardo_prueba_tecnica.Models;

namespace manuel_fajardo_prueba_tecnica.Controllers;

[ApiController]
[Route("[controller]")]
public class BranchController : Controller
{
  private readonly IBranchService _service;
  public BranchController(IBranchService service)
  {
    _service = service;
  }

  [HttpGet]
  public IEnumerable<Branch> GetPeople()
  {
    try
    {
      return _service.GetBranches();
    }
    catch (System.Exception)
    {
      throw;
    }
  }
}
