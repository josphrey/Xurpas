using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xurpas.Data;
using Xurpas.Models;

namespace Xurpas.Controllers
{
    public class ParkingSpacesController : Controller
    {
        private readonly ParkingContext _context;

        public ParkingSpacesController(ParkingContext context)
        {
            _context = context;
        }

        // GET: ParkingSpaces
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkingSpace.ToListAsync());
        }

        private void PopulateDropDownList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<ParkingType> pt = _context.ParkingType.Where(x => x.IsActive == true).ToList();

            pt.ForEach(x => list.Add(new SelectListItem(x.ParkingCode, x.ParkingCode)));

            ViewBag.ParkingTypeCD = list;
        }

        // GET: ParkingSpaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpace = await _context.ParkingSpace
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ParkingSpaceID == id);
            if (parkingSpace == null)
            {
                return NotFound();
            }

            return View(parkingSpace);
        }

        // GET: ParkingSpaces/Create
        public IActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        // POST: ParkingSpaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParkingSpaceID,ParkingTypeCode,IsAvailable,IsActive")] ParkingSpace parkingSpace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingSpace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkingSpace);
        }

        // GET: ParkingSpaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpace = await _context.ParkingSpace
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ParkingSpaceID == id);
            if (parkingSpace == null)
            {
                return NotFound();
            }
            PopulateDropDownList();
            return View(parkingSpace);
        }

        // POST: ParkingSpaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParkingSpaceID,ParkingTypeCode,IsAvailable,IsActive")] ParkingSpace parkingSpace)
        {
            if (id != parkingSpace.ParkingSpaceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingSpace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingSpaceExists(parkingSpace.ParkingSpaceID))
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
            return View(parkingSpace);
        }

        // GET: ParkingSpaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpace = await _context.ParkingSpace
                .FirstOrDefaultAsync(m => m.ParkingSpaceID == id);
            if (parkingSpace == null)
            {
                return NotFound();
            }

            return View(parkingSpace);
        }

        // POST: ParkingSpaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkingSpace = await _context.ParkingSpace.FindAsync(id);
            _context.ParkingSpace.Remove(parkingSpace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingSpaceExists(int id)
        {
            return _context.ParkingSpace.Any(e => e.ParkingSpaceID == id);
        }
    }
}
