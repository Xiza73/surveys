using System;
using System.Collections.Generic;

namespace manuel_fajardo_prueba_tecnica.Models;

public partial class Branch
{
    public int IdBranch { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Province { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<Survey> Surveys { get; } = new List<Survey>();
}
