using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BD_Films.Models;

namespace BD_Films.Pages.FilmsActors
{
    public class EditModel : PageModel
    {
        private readonly BD_Films.Models.DbFilmsContext _context;

        public EditModel(BD_Films.Models.DbFilmsContext context)
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

            var filmsactor =  await _context.FilmsActors.FirstOrDefaultAsync(m => m.Id == id);
            if (filmsactor == null)
            {
                return NotFound();
            }
            FilmsActor = filmsactor;
           ViewData["IdActor"] = new SelectList(_context.Actors, "Id", "Id");
           ViewData["IdFilm"] = new SelectList(_context.Films, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FilmsActor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmsActorExists(FilmsActor.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FilmsActorExists(long id)
        {
          return (_context.FilmsActors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
