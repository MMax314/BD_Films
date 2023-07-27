using System;
using System.Collections.Generic;

namespace BD_Films.Models;

public partial class Film
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? Budget { get; set; }

    public long? Year { get; set; }

    public string? Genre { get; set; }

    public virtual ICollection<FilmsActor> FilmsActors { get; set; } = new List<FilmsActor>();
}
