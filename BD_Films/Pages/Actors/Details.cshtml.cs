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
    public class DetailsModel : PageModel
    {
        private readonly BD_Films.Models.DbFilmsContext _context;

        public DetailsModel(BD_Films.Models.DbFilmsContext context)
        {
            _context = context;
        }

        public Actor Actor { get; set; } = default!;

        // Добавь это свойство
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;


        public async Task<IActionResult> OnGetAsync(long? id, int PageIndex = 1)
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

            this.PageIndex = PageIndex; // Присвой значение свойству

            return Page();
        }
    }
}
