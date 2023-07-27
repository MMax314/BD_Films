using System;
using System.Collections.Generic;

namespace BD_Films.Models;

public partial class Actor
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? YearBirth { get; set; }

    public virtual ICollection<FilmsActor> FilmsActors { get; set; } = new List<FilmsActor>();
}
