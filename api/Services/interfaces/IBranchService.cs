using manuel_fajardo_prueba_tecnica.Models;

namespace manuel_fajardo_prueba_tecnica.Services.interfaces;

public interface IBranchService
{
  public IEnumerable<Branch> GetBranches();
}
