using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_Films.Models;

public partial class Actor
{
    //[HiddenInput]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //[Required]

    public long Id { get; set; }

    public string Name { get; set; } = null!;

    [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public double? YearBirth { get; set; }
    //[Column(TypeName = "REAL")]
    //public DateTime YearBirth { get; set; }

    public virtual ICollection<FilmsActor> FilmsActors { get; set; } = new List<FilmsActor>();
}