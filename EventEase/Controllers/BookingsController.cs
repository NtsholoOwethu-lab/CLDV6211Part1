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
        public IActionResult Create()
        {
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

            return View(bookings);
        }

        public async Task<IActionResult> Details(int? id) // if error check here - id / eventid
        {
            var bookings = await _context.Bookings.FirstOrDefaultAsync(x => x.EventId == id);

            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var bookings = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == id);

            if (bookings == null)
            {
                return NotFound();
            }
            return View();


        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            return View(bookings);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, Bookings bookings)
        {
            if (id != bookings.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(bookings.BookingId))
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
            return View(bookings);
        }
    }
}