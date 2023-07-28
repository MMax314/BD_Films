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
    public class IndexModel : PageModel
    {
        private readonly BD_Films.Models.DbFilmsContext _context;

        public IndexModel(BD_Films.Models.DbFilmsContext context)
        {
            _context = context;
        }

        public IList<Actor> Actor { get; set; } = default!;

        public async Task OnGetAsync(string? searchName, double? yearBirth)
        {
            IQueryable<Actor> actorsQuery = _context.Actors;

            if (!string.IsNullOrEmpty(searchName))
            {
                actorsQuery = actorsQuery.Where(a => a.Name.Contains(searchName));
            }

            if (yearBirth.HasValue)
            {
                actorsQuery = actorsQuery.Where(a => a.YearBirth == yearBirth.Value);
            }

            Actor = await actorsQuery.ToListAsync();

            /*
            Эти строки нужны для того, чтобы сохранить параметры фильтрации в свойствах страницы.
            Это позволяет передать их обратно на страницу при повторном запросе, 
            чтобы пользователь мог видеть, какие фильтры он применил, и изменить их при необходимости. 
            Это также позволяет сохранить состояние фильтрации при переходе на другие страницы 
            или возврате на текущую страницу. 
            */
            searchName = searchName;
            yearBirth = yearBirth;
        }
    }
}
