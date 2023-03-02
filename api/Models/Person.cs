using System;
using System.Collections.Generic;

namespace manuel_fajardo_prueba_tecnica.Models;

public partial class Person
{
    public int IdPerson { get; set; }

    public string Identification { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Sex { get; set; } = null!;

    public int Age { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Survey> Surveys { get; } = new List<Survey>();
}
