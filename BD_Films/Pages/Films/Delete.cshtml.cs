using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BD_Films.Models;

namespace BD_Films.Pages.Films
{
    public class DeleteModel : PageModel
    {
        private readonly BD_Films.Models.DbFilmsContext _context;

        public DeleteModel(BD_Films.Models.DbFilmsContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Film Film { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FirstOrDefaultAsync(m => m.Id == id);

            if (film == null)
            {
                return NotFound();
            }
            else 
            {
                Film = film;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }
            var film = await _context.Films.FindAsync(id);

            if (film != null)
            {
                Film = film;
                _context.Films.Remove(Film);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
