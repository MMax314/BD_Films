/*
Эти строки нужны для того, чтобы сохранить параметры фильтрации в свойствах страницы.
Это позволяет передать их обратно на страницу при повторном запросе, 
чтобы пользователь мог видеть, какие фильтры он применил, и изменить их при необходимости. 
Это также позволяет сохранить состояние фильтрации при переходе на другие страницы 
или возврате на текущую страницу. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BD_Films.Models;
using Azure;

namespace BD_Films.Pages.Actors
{
    public class IndexModel : PageModel
    {
        //[Begin] Bing
        public int PageSize { get; set; } = 100; // Количество элементов на странице
        public int TotalItems { get; set; } // Общее количество элементов
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;  // Номер текущей страницы
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems, PageSize)); // Общее количество страниц

        // Метод для получения подмножества элементов для текущей страницы
        public IEnumerable<Actor> PaginatedActors => Actor
        .Skip((PageIndex - 1) * PageSize)
        .Take(PageSize);

        // Метод для проверки, есть ли предыдущая страница
        public bool HasPreviousPage => PageIndex > 1;

        // Метод для проверки, есть ли следующая страница
        public bool HasNextPage => PageIndex < TotalPages;

        // Добавь эти два свойства
        [BindProperty(SupportsGet = true)]
        public string? searchName { get; set; }
        [BindProperty(SupportsGet = true)]
        public double? yearBirth { get; set; }
        //[End] Bing

        private readonly BD_Films.Models.DbFilmsContext _context;

        public IndexModel(BD_Films.Models.DbFilmsContext context)
        {
            _context = context;
        }

        public IList<Actor> Actor { get; set; } = default!;

        //public async Task OnGetAsync(string? searchName, double? yearBirth, int page = 1)
        public async Task OnGetAsync(string? searchName, double? yearBirth, int PageIndex = 1)        
        //public async Task OnGetAsync(string? searchName, DateTime yearBirth, int PageIndex = 1)
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

            TotalItems = await actorsQuery.CountAsync(); // Получаем общее количество элементов
            //PageIndex = page; // Устанавливаем номер текущей страницы
            this.PageIndex = PageIndex;
            Actor = await actorsQuery.ToListAsync(); // Получаем все элементы

            this.searchName = searchName;
            this.yearBirth = yearBirth;
        }
    }
}
