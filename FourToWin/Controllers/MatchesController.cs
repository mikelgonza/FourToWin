using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FourToWin.Data;
using FourToWin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FourToWin.Controllers
{
    [Authorize]
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MatchesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Match(string lobbyAction)
        {
            return View("Match", lobbyAction);
        }

        public async Task<IActionResult> SaveMatchData([Bind("Id,User1Id,User2Id,Winner,NumRounds")] Match match)
        {

            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
            }

            return View(match);
        }

        public async Task<IActionResult> Statistics()
        {
            string id = _userManager.GetUserId(User);

            ViewData["UsersList"] = _context.Users;

            // Matches played
            List<Match> playedU1 = await _context.Match.Where(x => x.User1Id == id).ToListAsync();
            List<Match> playedU2 = await _context.Match.Where(x => x.User2Id == id).ToListAsync();

            ViewData["playedU1"] = playedU1;
            ViewData["playedU2"] = playedU2;

            // Won
            List<Match> wonU1 = await _context.Match.Where(x => x.User1Id == id && x.Winner == "1").ToListAsync();
            List<Match> wonU2 = await _context.Match.Where(x => x.User2Id == id && x.Winner == "2").ToListAsync();
            ViewData["won"] = wonU1.Count + wonU2.Count;

            // Lost
            List<Match> lostU1 = await _context.Match.Where(x => x.User1Id == id && x.Winner == "2").ToListAsync();
            List<Match> lostU2 = await _context.Match.Where(x => x.User2Id == id && x.Winner == "1").ToListAsync();
            ViewData["lost"] = lostU1.Count + lostU2.Count;

            // Draw
            List<Match> drawU1 = await _context.Match.Where(x => x.User1Id == id && x.Winner == "x").ToListAsync();
            List<Match> drawU2 = await _context.Match.Where(x => x.User2Id == id && x.Winner == "x").ToListAsync();
            ViewData["draw"] = drawU1.Count + drawU2.Count;

            // Total
            List<Match> totalU1 = await _context.Match.Where(x => x.User1Id == id).ToListAsync();
            List<Match> totalU2 = await _context.Match.Where(x => x.User2Id == id).ToListAsync();
            ViewData["total"] = totalU1.Count + totalU2.Count;

            // Record of movements
            int nU1 = 21;
            int nU2 = 21;
            int n;

            foreach (var item in wonU1)
            {
                if (item.NumRounds < nU1) nU1 = item.NumRounds;
            }

            foreach (var item in wonU2)
            {
                if (item.NumRounds < nU2) nU2 = item.NumRounds;
            }

            if (nU1 < nU2)
                n = nU1;
            else
                n = nU2;

            ViewData["record"] = n;

            return View();
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Match.Include(m => m.User1).Include(m => m.User2);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.User1)
                .Include(m => m.User2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["User1Id"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["User2Id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User1Id,User2Id,Winner,NumRounds")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["User1Id"] = new SelectList(_context.Users, "Id", "Id", match.User1Id);
            ViewData["User2Id"] = new SelectList(_context.Users, "Id", "Id", match.User2Id);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["User1Id"] = new SelectList(_context.Users, "Id", "Id", match.User1Id);
            ViewData["User2Id"] = new SelectList(_context.Users, "Id", "Id", match.User2Id);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User1Id,User2Id,Winner,NumRounds")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            ViewData["User1Id"] = new SelectList(_context.Users, "Id", "Id", match.User1Id);
            ViewData["User2Id"] = new SelectList(_context.Users, "Id", "Id", match.User2Id);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.User1)
                .Include(m => m.User2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Match.FindAsync(id);
            _context.Match.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.Id == id);
        }
    }
}
