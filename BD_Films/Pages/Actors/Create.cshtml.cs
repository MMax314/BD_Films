using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BD_Films.Models;

namespace BD_Films.Pages.Actors
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
            return Page();
        }

        [BindProperty]
        public Actor Actor { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //Вопрос: почему не проходит валидация модели?
            //Ответ: потому что в модели Actor нет атрибутов валидации
            //Вопрос: почему ModelState.IsValid==false?
            //Ответ: потому что валидация модели не прошла
            //Вопрос: как сделать так, чтобы валидация модели прошла если поле ID будет пустым?
            //Ответ: добавить атрибуты валидации в модель Actor
            //Вопрос: В какой файл проекта нужно добавлять атрибуты валидации?
            //Ответ: в файл модели Actor.cs
            //Вопрос: Какой атрибут для валидации нужно добавить. чтобы поле ID могло быть пустым?
            //Ответ: [Required]
            if (!ModelState.IsValid || _context.Actors == null || Actor == null)
            {
                return Page();
            }

            _context.Actors.Add(Actor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
