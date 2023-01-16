using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Controllers;
using Projekt.Models;


namespace Projekt.ViewComponents
{
    public class GenreCountViewComponent : ViewComponent
    {
        private readonly projektContext _context;
        public GenreCountViewComponent(projektContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var liczba = _context.Genres.Count();
            ViewData["genrescount"] = liczba;
            return View("Default");
        }

    }
}