﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BD_Films.Models;

namespace BD_Films.Pages.Actors
{
    public class IndexModel : PageModel
    {
        private readonly BD_Films.Models.DbFilmsContext _context;

        public IndexModel(BD_Films.Models.DbFilmsContext context)
        {
            _context = context;
        }

        public IList<Actor> Actor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Actors != null)
            {
                Actor = await _context.Actors.ToListAsync();
            }
        }
    }
}
