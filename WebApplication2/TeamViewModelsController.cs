using ASPNETCORE.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2
{
    public class TeamViewModelsController : Controller
    {
        private readonly WebApplication2Context _context;

        public TeamViewModelsController(WebApplication2Context context)
        {
            _context = context;
        }

        // GET: TeamViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TeamViewModel.ToListAsync());
        }

        // GET: TeamViewModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamViewModel = await _context.TeamViewModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (teamViewModel == null)
            {
                return NotFound();
            }

            return View(teamViewModel);
        }

        // GET: TeamViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ID")] TeamViewModel teamViewModel)
        {
            if (ModelState.IsValid)
            {
                teamViewModel.ID = Guid.NewGuid();
                _context.Add(teamViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamViewModel);
        }

        // GET: TeamViewModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamViewModel = await _context.TeamViewModel.FindAsync(id);
            if (teamViewModel == null)
            {
                return NotFound();
            }
            return View(teamViewModel);
        }

        // POST: TeamViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,ID")] TeamViewModel teamViewModel)
        {
            if (id != teamViewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamViewModelExists(teamViewModel.ID))
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
            return View(teamViewModel);
        }

        // GET: TeamViewModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamViewModel = await _context.TeamViewModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (teamViewModel == null)
            {
                return NotFound();
            }

            return View(teamViewModel);
        }

        // POST: TeamViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var teamViewModel = await _context.TeamViewModel.FindAsync(id);
            _context.TeamViewModel.Remove(teamViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamViewModelExists(Guid id)
        {
            return _context.TeamViewModel.Any(e => e.ID == id);
        }
    }
}
