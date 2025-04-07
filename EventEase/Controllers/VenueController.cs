using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDBContext _context;

        public VenueController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venue.ToListAsync();

            return View(venues);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }
    }
}
