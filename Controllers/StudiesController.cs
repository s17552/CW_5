using System.Linq;
using System.Threading.Tasks;
using CW_5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CW_5.Controllers
{
    public class StudiesController : Controller
    {
        private readonly MasterContext _context;

        public StudiesController(MasterContext context)
        {
            _context = context;
        }

        // GET: Studies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Studies.ToListAsync());
        }

        // GET: Studies/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studies = await _context.Studies
                .FirstOrDefaultAsync(m => m.IdStudy == id);
            if (studies == null)
            {
                return NotFound();
            }

            return View(studies);
        }

        // GET: Studies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdStudy,Name")] Studies studies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studies);
        }

        // GET: Studies/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studies = await _context.Studies.FindAsync(id);
            if (studies == null)
            {
                return NotFound();
            }
            return View(studies);
        }

        // POST: Studies/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdStudy,Name")] Studies studies)
        {
            if (id != studies.IdStudy)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudiesExists(studies.IdStudy))
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
            return View(studies);
        }

        // GET: Studies/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studies = await _context.Studies
                .FirstOrDefaultAsync(m => m.IdStudy == id);
            if (studies == null)
            {
                return NotFound();
            }

            return View(studies);
        }

        // POST: Studies/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studies = await _context.Studies.FindAsync(id);
            _context.Studies.Remove(studies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool StudiesExists(int id)
        {
            return _context.Studies.Any(e => e.IdStudy == id);
        }
    }
}
