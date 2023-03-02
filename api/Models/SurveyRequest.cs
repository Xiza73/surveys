namespace manuel_fajardo_prueba_tecnica.Models;

public partial class SurveyRequest
{
  public int IdBranch { get; set; }

  public int idPerson { get; set; }

  public string Questions { get; set; } = null!;
}
