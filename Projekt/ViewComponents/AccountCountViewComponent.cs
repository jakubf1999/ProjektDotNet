using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Controllers;
using Projekt.Models;


namespace Projekt.ViewComponents
{
    public class AccountCountViewComponent : ViewComponent
    {
        private readonly projektContext _context;
        public AccountCountViewComponent(projektContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var liczba = _context.Accounts.Count();
            ViewData["accountscount"] = liczba;
            return View("Default");
        }

    }
}