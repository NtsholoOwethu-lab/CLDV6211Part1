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

        public async Task<IActionResult> Index(string searchString)
        {
            var bookings =  _context.Bookings
            .Include(i => i.Venue)
            .Include(i => i.EventDetails)
            .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b =>
                b.Venue.VenueName.Contains(searchString) ||
                b.EventDetails.EventName.Contains(searchString));
            }


            return View(await bookings.ToListAsync());
        }
        //create button
        public IActionResult Create()
        {
            ViewBag.Venue = _context.Venue.ToList();
            ViewBag.EventDetails = _context.EventDetails.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bookings booking)
        {
            var selectedEvent = await _context.EventDetails.FirstOrDefaultAsync(e => e.EventId == booking.EventId);

            if (selectedEvent == null)
            {
                ModelState.AddModelError("", "Selected event not found.");
                ViewData["Events"] = _context.EventDetails.ToList();
                ViewData["Venues"] = _context.Venue.ToList();
                return View(booking);
            }

            // Check manually for double booking
            var conflict = await _context.Bookings
                .Include(b => b.EventDetails)
                .AnyAsync(b => b.VenueId == booking.VenueId &&
                               b.EventDetails.EventDate.Date == selectedEvent.EventDate.Date);

            if (conflict)
            {
                ModelState.AddModelError("", "This venue is already booked for that date.");
                ViewData["EventDetails"] = _context.EventDetails.ToList();
                ViewData["Venue"] = _context.Venue.ToList();
                return View(booking);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Booking created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // If database constraint fails (e.g., unique key violation), show friendly message
                    ModelState.AddModelError("", "This venue is already booked for that date.");
                    ViewData["EventDetails"] = _context.EventDetails.ToList();
                    ViewData["Venue"] = _context.Venue.ToList();
                    return View(booking);
                }
            }

            ViewData["EventDetails"] = _context.EventDetails.ToList();
            ViewData["Venue"] = _context.Venue.ToList();
            return View(booking);
        }
    }
}