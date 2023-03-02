using System;
using System.Collections.Generic;

namespace manuel_fajardo_prueba_tecnica.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
