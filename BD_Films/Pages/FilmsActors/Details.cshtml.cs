using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BD_Films.Models;

namespace BD_Films.Pages.FilmsActors
{
    public class DetailsModel : PageModel
    {
        private readonly BD_Films.Models.DbFilmsContext _context;

        public DetailsModel(BD_Films.Models.DbFilmsContext context)
        {
            _context = context;
        }

      public FilmsActor FilmsActor { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.FilmsActors == null)
            {
                return NotFound();
            }

            var filmsactor = await _context.FilmsActors.FirstOrDefaultAsync(m => m.Id == id);
            if (filmsactor == null)
            {
                return NotFound();
            }
            else 
            {
                FilmsActor = filmsactor;
            }
            return Page();
        }
    }
}
