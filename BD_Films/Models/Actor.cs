using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BD_Films.Models;

public partial class Actor
{
    [HiddenInput]
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public double? YearBirth { get; set; }

    public virtual ICollection<FilmsActor> FilmsActors { get; set; } = new List<FilmsActor>();
}