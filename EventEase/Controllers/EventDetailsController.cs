using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EventEase.Controllers
{
    public class EventDetailsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EventDetailsController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var eventdetails = await _context.EventDetails.ToListAsync();

            return View(eventdetails);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventDetails @eventDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@eventDetails);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event created successfully.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["Venue"] = _context.Venue.ToList();

            return View(@eventDetails);
        }
        public async Task<IActionResult> Details(int? id) // if error check here - id / eventid
        {
            if (id == null) return NotFound();

            var @event = await _context.EventDetails
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(v => v.EventId == id);
            if (@event == null) return NotFound();

            return View(@event);
        }

        //STEP 1: DELETION
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.EventDetails
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(v => v.VenueId == id);
            if (@event == null) return NotFound();

            return View(@event);
        }
        //STEP 2 DELETION PERFORMED
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.EventDetails.FindAsync(id);
            if (@event == null) return NotFound();

            var isBooked = await _context.Bookings.AnyAsync(b => b.EventId == id);
            if (isBooked)
            {
                TempData["ErrorMessage"] = "Cannot delete event because it has existing bookings.";
                return RedirectToAction(nameof(Index));
            }

            _context.EventDetails.Remove(@event);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Event deleted successfully.";
            return RedirectToAction(nameof(Index));

        }

        //edit(needed)
        private bool CompanyExists(int id)
        {
            return _context.EventDetails.Any(e => e.EventId == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @eventdetails = await _context.EventDetails.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Venue"] = _context.Venue.ToList();
            return View(@eventdetails);
        }
        //Edit
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, EventDetails eventdetails)
        {
            if (id != @eventdetails.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@eventdetails);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Event updated successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(eventdetails.EventId))
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
            return View(@eventdetails);
        }

    }
}
