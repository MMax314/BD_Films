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
    public class DeleteModel : PageModel
    {
        private readonly BD_Films.Models.DbFilmsContext _context;

        public DeleteModel(BD_Films.Models.DbFilmsContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.FilmsActors == null)
            {
                return NotFound();
            }
            var filmsactor = await _context.FilmsActors.FindAsync(id);

            if (filmsactor != null)
            {
                FilmsActor = filmsactor;
                _context.FilmsActors.Remove(FilmsActor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
