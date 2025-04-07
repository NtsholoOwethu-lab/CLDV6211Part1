using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using StudentInterships.Models;

namespace StudentInterships.Controllers
{
    public class InternshipController : Controller
    {
        private readonly ApplicationDBContext _context;

        public InternshipController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var interships = await _context.Internships
                .Include(i => i.Student)
                .Include(i => i.Company)
                .ToListAsync();

            return View(interships);
        }

        public IActionResult Create()
        {
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Company = _context.Company.ToList();
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Internship internship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(internship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Students = _context.Students.ToList();
            ViewBag.Company = _context.Company.ToList();
            return View(internship);
        }

        public async Task<IActionResult> LogHours(int id)
        {
            var internship = await _context.Internships.FindAsync(id);
            if (internship == null) return NotFound();
            return View(internship);
        }
        [HttpPost]

        public async Task<IActionResult> LogHours(int id, Internship model)
        {
            var internship = await _context.Internships.FindAsync(id);
            if (internship == null) return NotFound();

            internship.SupervisorFeedback = model.SupervisorFeedback;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
