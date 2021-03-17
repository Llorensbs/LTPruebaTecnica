using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    public class HotelTypesController : Controller
    {
        private readonly LTPruebaTecnicaContext _context;

        public HotelTypesController(LTPruebaTecnicaContext context)
        {
            _context = context;
        }

        // GET: HotelTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.HotelType.ToListAsync());
        }

        // GET: HotelTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelType = await _context.HotelType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelType == null)
            {
                return NotFound();
            }

            return View(hotelType);
        }

        // GET: HotelTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HotelTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Template")] HotelType hotelType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotelType);
        }

        // GET: HotelTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelType = await _context.HotelType.FindAsync(id);
            if (hotelType == null)
            {
                return NotFound();
            }
            return View(hotelType);
        }

        // POST: HotelTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Template")] HotelType hotelType)
        {
            if (id != hotelType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelTypeExists(hotelType.Id))
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
            return View(hotelType);
        }

        // GET: HotelTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelType = await _context.HotelType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelType == null)
            {
                return NotFound();
            }

            return View(hotelType);
        }

        // POST: HotelTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotelType = await _context.HotelType.FindAsync(id);
            _context.HotelType.Remove(hotelType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelTypeExists(int id)
        {
            return _context.HotelType.Any(e => e.Id == id);
        }
    }
}
