using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BD_Films.Models;

namespace BD_Films.Pages.FilmsActors
{
    public class CreateModel : PageModel
    {
        private readonly BD_Films.Models.DbFilmsContext _context;

        public CreateModel(BD_Films.Models.DbFilmsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdActor"] = new SelectList(_context.Actors, "Id", "Id");
        ViewData["IdFilm"] = new SelectList(_context.Films, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public FilmsActor FilmsActor { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.FilmsActors == null || FilmsActor == null)
            {
                return Page();
            }

            _context.FilmsActors.Add(FilmsActor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
