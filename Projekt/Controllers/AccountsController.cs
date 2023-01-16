using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class AccountsController : Controller
    {
        private readonly projektContext _context;

        public AccountsController(projektContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return _context.Accounts != null ?
                View(await _context.Accounts.Include(a => a.User).Include(a => a.Games).ThenInclude(a => a.Reviews).ToListAsync()) : Problem("Entity set 'projektContext.Accounts' is null.");
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.User)
                .Include(a => a.Games).ThenInclude(a => a.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FL");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Password,Email,UserId")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FL", account.UserId);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FL", account.UserId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Password,Email,UserId")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FL", account.UserId);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'projektContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //Metoda do formularza recenzji
        public async Task<IActionResult> TakeReviews(int id)
        {
            var account = await _context.Accounts.SingleAsync(c => c.Id == id);
            var lista = new List<GR>();
            var xgame = _context.Games.Include(g => g.Accounts);
            foreach (var g in xgame)
            {
                if (g.Accounts.Contains(account))
                {
                    lista.Add(new GR
                    {
                        GameId = g.Id,
                        GameName = g.Name,
                        Score = 0
                    }
                );
                }
            }
            foreach (var g in lista)
            {
                var xtemp = _context.Reviews.Where(R => R.AccountId == id & R.GameId == g.GameId);
                if (xtemp.Count() > 0)
                {
                    var xg = _context.Reviews.Where(R => R.AccountId == account.Id & R.GameId == g.GameId).First();
                    if (xg != null) { g.Score = xg.Score; }
                }
            }
            ViewData["scorelist"] = lista;
            return View(account);
        }

        //Metoda do zapisywania recenzji
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveReviews(int id)
        {
            var account = await _context.Accounts.SingleAsync(c => c.Id == id);
            var xaccounts = HttpContext.Request.Form["gamelist"];
            var xreviews = HttpContext.Request.Form["scorelist"];
            var ile = xaccounts.Count();
            for (int i = 0; i < ile; i++)
            {
                var xaid = int.Parse(xaccounts[i]);
                var xsc = decimal.Parse(xreviews[i]);
                var xreview = _context.Reviews.Where(r => r.AccountId == id & r.GameId == xaid);
                if (xreview.Any())
                {
                    var xocena = _context.Reviews.Where(r => r.AccountId == id & r.GameId == xaid).Single();
                    xocena.Score = xsc;
                    _context.Update(xocena);
                }
                else
                {
                    var xocena = new Review();
                    xocena.AccountId = id;
                    xocena.GameId = xaid;
                    xocena.Score = xsc;
                    _context.Add(xocena);
                }
            }
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Details", new { id = id });
        }
    }
}
