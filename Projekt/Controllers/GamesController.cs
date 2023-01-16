using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class GamesController : Controller
    {
        private readonly projektContext _context;

        public GamesController(projektContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var projektContext = _context.Games.Include(g => g.Publisher).Include(g => g.Genres).Include(g => g.Accounts);
            return View(await projektContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,PublisherId")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", game.PublisherId);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.Include(g => g.Publisher).Include(g => g.Genres).Include(g => g.Accounts).SingleAsync(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", game.PublisherId);
            GetGenreList(id);
            GetAccountList(id);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,PublisherId")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    var xgenres = HttpContext.Request.Form["selectedGenres"];
                    var xaccounts = HttpContext.Request.Form["selectedAccounts"];
                    var xgg = await _context.Games.Include(g => g.Genres).SingleAsync(g => g.Id == game.Id);
                    var xga = await _context.Games.Include(a => a.Accounts).SingleAsync(a => a.Id == game.Id);
                    if (xgg != null) { xgg.Genres.Clear(); } else { xgg.Genres = new List<Genre>(); };
                    foreach(var h in xgenres)
                    {
                        var xwyb = await _context.Genres.SingleAsync(xg => xg.Id == int.Parse(h));
                        xgg.Genres.Add(xwyb);
                    }
                    if (xga.Accounts != null) { xga.Accounts.Clear(); } else { xga.Accounts = new List<Account>(); };
                    foreach (var b in xaccounts)
                    {
                        var xwyba = await _context.Accounts.SingleAsync(xa => xa.Id == int.Parse(b));
                        xga.Accounts.Add(xwyba);
                    }
                    _context.Update(xgg);
                    _context.Update(xga);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", game.PublisherId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Games == null)
            {
                return Problem("Entity set 'projektContext.Games'  is null.");
            }
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void GetGenreList(int? id = 0)
        {
            var GenresAll = _context.Genres;
            var SelectedGenres = new List<GG>();
            var xga = _context.Games.Include(g => g.Genres).Single(g => g.Id == id);
            var xch = "";
            foreach (var h in GenresAll)
            {

                if (xga.Genres.Contains(h)) { xch = "checked"; } else { xch = ""; };
                SelectedGenres.Add(
                    new GG
                    {
                        GenreId = h.Id,
                        Nazwa = h.Name,
                        Checked = xch
                    }
                );
            }
            ViewData["genres"] = SelectedGenres;

        }

        private void GetAccountList(int? id = 0)
        {
            var AccountsAll = _context.Accounts;
            var SelectedAccounts = new List<GA>();
            var xst = _context.Games.Include(g => g.Accounts).Single(g => g.Id == id);
            var xch = "";
            foreach (var a in AccountsAll)
            {

                if (xst.Accounts.Contains(a)) { xch = "checked"; } else { xch = ""; };
                SelectedAccounts.Add(
                    new GA
                    {
                        AccountId = a.Id,
                        Name = a.Login,
                        Checked = xch
                    }
                );
            }
            ViewData["accounts"] = SelectedAccounts;

        }
        private bool GameExists(int id)
        {
          return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
