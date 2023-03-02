using System;
using System.Collections.Generic;

namespace manuel_fajardo_prueba_tecnica.Models;

public partial class Question
{
    public int IdQuestion { get; set; }

    public int IdCategory { get; set; }

    public string Content { get; set; } = null!;

    public int Type { get; set; }

    public bool? Active { get; set; }

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual ICollection<SurveyQuestion> SurveyQuestions { get; } = new List<SurveyQuestion>();
}
