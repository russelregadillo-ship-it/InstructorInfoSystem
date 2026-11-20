
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using russelregadillo.Data;
using russelregadillo.Models;

namespace russelregadillo.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // READ - Display all instructors
        public async Task<IActionResult> Index()
        {
            var instructors = await _context.Instructors
                .AsNoTracking()
                .OrderBy(i => i.FullName)
                .ToListAsync();

            return View(instructors);
        }

        // CREATE - Display form
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Instructor());
        }

        // CREATE - Save instructor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return View(instructor);
            }

            instructor.FullName = instructor.FullName.Trim();
            instructor.EmailAddress = instructor.EmailAddress.Trim().ToLowerInvariant();

            bool emailExists = await _context.Instructors
                .AnyAsync(i => i.EmailAddress.ToLower() == instructor.EmailAddress);

            if (emailExists)
            {
                ModelState.AddModelError("EmailAddress",
                    "This email address is already registered.");

                return View(instructor);
            }

            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Instructor added successfully!";

            return RedirectToAction(nameof(Index));
        }

        // DETAILS - View instructor information
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(i => i.InstructorId == id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // EDIT - Display edit form
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // EDIT - Save changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor)
        {
            if (id != instructor.InstructorId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(instructor);
            }

            instructor.FullName = instructor.FullName.Trim();
            instructor.EmailAddress = instructor.EmailAddress.Trim().ToLowerInvariant();

            bool emailExists = await _context.Instructors
                .AnyAsync(i => i.EmailAddress.ToLower() == instructor.EmailAddress
                    && i.InstructorId != id);

            if (emailExists)
            {
                ModelState.AddModelError("EmailAddress",
                    "This email address is already registered.");

                return View(instructor);
            }

            try
            {
                _context.Update(instructor);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Instructor updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Instructors.AnyAsync(
                    i => i.InstructorId == id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // DELETE - Display confirmation
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(i => i.InstructorId == id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // DELETE - Confirm deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Instructor deleted successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}
