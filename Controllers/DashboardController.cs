
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using russelregadillo.Data;

namespace russelregadillo.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int totalInstructors = await _context.Instructors.CountAsync();

            ViewBag.TotalInstructors = totalInstructors;

            return View();
        }
    }
}
