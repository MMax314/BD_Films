using System;
using System.Collections.Generic;

namespace BD_Films.Models;

public partial class FilmsActor
{
    public long? IdFilm { get; set; }

    public long? IdActor { get; set; }

    public string? NameHero { get; set; }

    public virtual Actor? IdActorNavigation { get; set; }

    public virtual Film? IdFilmNavigation { get; set; }
}
