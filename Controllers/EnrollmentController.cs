using System.Linq;
using System.Threading.Tasks;
using CW_5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CW_5.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly MasterContext _context;

        public EnrollmentController(MasterContext context)
        {
            _context = context;
        }

        // GET: Enrollment
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Enrollment.Include(e => e.IdStudyNavigation);
            return View(await masterContext.ToListAsync());
        }

        // GET: Enrollment/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.IdStudyNavigation)
                .FirstOrDefaultAsync(m => m.IdEnrollment == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollment/Create
        public IActionResult Create()
        {
            ViewData["IdStudy"] = new SelectList(_context.Studies, "IdStudy", "Name");
            return View();
        }

        // POST: Enrollment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Semester,IdStudy,StartDate,IdEnrollment")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdStudy"] = new SelectList(_context.Studies, "IdStudy", "Name", enrollment.IdStudy);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["IdStudy"] = new SelectList(_context.Studies, "IdStudy", "Name", enrollment.IdStudy);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Semester,IdStudy,StartDate,IdEnrollment")] Enrollment enrollment)
        {
            if (id != enrollment.IdEnrollment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.IdEnrollment))
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
            ViewData["IdStudy"] = new SelectList(_context.Studies, "IdStudy", "Name", enrollment.IdStudy);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.IdStudyNavigation)
                .FirstOrDefaultAsync(m => m.IdEnrollment == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollment/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollment.FindAsync(id);
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.IdEnrollment == id);
        }
    }
}
