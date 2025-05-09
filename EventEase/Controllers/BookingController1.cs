using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EventEase.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public BookingsController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
            .Include(i => i.Venue)
            .Include(i => i.EventDetails)
            .ToListAsync();

            return View(bookings);
        }
        //create button
        public IActionResult Create()
        {
            ViewBag.Venue = _context.Venue.ToList();
            ViewBag.EventDetails = _context.EventDetails.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venue = _context.Venue.ToList();
            ViewBag.EventDetails = _context.EventDetails.ToList();
            return View(bookings);
        }
    }
}