using System;
using System.Collections.Generic;

namespace manuel_fajardo_prueba_tecnica.Models;

public partial class Survey
{
    public int IdSurvey { get; set; }

    public int IdBranch { get; set; }

    public int IdPerson { get; set; }

    public DateTime? Date { get; set; }

    public bool? Active { get; set; } = true;

    public int Percentage { get; set; }

    public virtual Branch IdBranchNavigation { get; set; } = null!;

    public virtual Person IdPersonNavigation { get; set; } = null!;

    public virtual ICollection<SurveyQuestion> SurveyQuestions { get; } = new List<SurveyQuestion>();
}
