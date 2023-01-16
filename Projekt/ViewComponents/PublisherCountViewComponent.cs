using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Controllers;
using Projekt.Models;


namespace Projekt.ViewComponents
{
    public class PublisherCountViewComponent : ViewComponent
    {
        private readonly projektContext _context;
        public PublisherCountViewComponent(projektContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var liczba = _context.Publishers.Count();
            ViewData["publisherscount"] = liczba;
            return View("Default");
        }

    }
}