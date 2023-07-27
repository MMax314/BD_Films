using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BD_Films.Models;

namespace BD_Films.Pages.Actors
{
    public class DeleteModel : PageModel
    {
        private readonly BD_Films.Models.DbFilmsContext _context;

        public DeleteModel(BD_Films.Models.DbFilmsContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Actor Actor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FirstOrDefaultAsync(m => m.Id == id);

            if (actor == null)
            {
                return NotFound();
            }
            else 
            {
                Actor = actor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }
            var actor = await _context.Actors.FindAsync(id);

            if (actor != null)
            {
                Actor = actor;
                _context.Actors.Remove(Actor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
