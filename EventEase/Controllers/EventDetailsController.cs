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
        public async Task<IActionResult> Create(EventDetails eventDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(eventDetails);
        }
        public async Task<IActionResult> Details(int? id) // if error check here - id / eventid
        {
            var eventdetails = await _context.EventDetails.FirstOrDefaultAsync(x => x.EventId == id);

            if (eventdetails == null)
            {
                return NotFound();
            }
            return View(eventdetails);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var eventdetails = await _context.EventDetails.FirstOrDefaultAsync(x => x.EventId == id);

            if (eventdetails == null)
            {
                return NotFound();
            }
            return View();


        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var company = await _context.EventDetails.FindAsync(id);
            _context.EventDetails.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //edit
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

            var eventdetails = await _context.EventDetails.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            return View(eventdetails);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id, EventDetails eventdetails)
        {
            if (id != eventdetails.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventdetails);
                    await _context.SaveChangesAsync();
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
            return View(eventdetails);
        }

    }
}
