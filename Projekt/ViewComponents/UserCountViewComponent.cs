using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Controllers;
using Projekt.Models;


namespace Projekt.ViewComponents
{
    public class UserCountViewComponent : ViewComponent
    {
        private readonly projektContext _context;
        public UserCountViewComponent(projektContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var liczba = _context.Users.Count();
            ViewData["userscount"] = liczba;
            return View("Default");
        }

    }
}