using System;
using System.Collections.Generic;

namespace manuel_fajardo_prueba_tecnica.Models;

public partial class SurveyQuestion
{
    public int IdSurvey { get; set; }

    public int IdQuestion { get; set; }

    public string Answer { get; set; } = null!;

    public int Percentage { get; set; }

    public virtual Question IdQuestionNavigation { get; set; } = null!;

    public virtual Survey IdSurveyNavigation { get; set; } = null!;
}
