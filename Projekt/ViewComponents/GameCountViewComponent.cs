using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Controllers;
using Projekt.Models;


namespace Projekt.ViewComponents
{
    public class GameCountViewComponent : ViewComponent
    {
        private readonly projektContext _context;
        public GameCountViewComponent(projektContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var liczba = _context.Games.Count();
            ViewData["gamescount"] = liczba;
            return View("Default");
        }

    }
}