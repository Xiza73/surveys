using manuel_fajardo_prueba_tecnica.Services.interfaces;
using manuel_fajardo_prueba_tecnica.Models;
using Microsoft.EntityFrameworkCore;

namespace manuel_fajardo_prueba_tecnica.Services;

public class BranchService : IBranchService
{
  private readonly DbRetailContext _context;

  public BranchService(DbRetailContext context)
  {
    _context = context;
  }

  public IEnumerable<Branch> GetBranches()
  {
    var result = _context.Database.ExecuteSqlRaw("EXEC sp_get_branches");
    Console.WriteLine(result);
    return _context.Branches?.ToList() ?? new List<Branch>();
  }
}
