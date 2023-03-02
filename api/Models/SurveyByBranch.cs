using System;
using System.Collections.Generic;

namespace manuel_fajardo_prueba_tecnica.Models;

public partial class SurveyByBranch
{
    public int IdSurvey { get; set; }

    public DateTime? Date { get; set; }

    public int Percentage { get; set; }

    public string Branch { get; set; } = null!;

    public string Person { get; set; } = null!;
}
