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
    public class ParkingSpacePerEntryPointsController : Controller
    {
        private readonly ParkingContext _context;

        public ParkingSpacePerEntryPointsController(ParkingContext context)
        {
            _context = context;
        }

        // GET: ParkingSpacePerEntryPoints
        public async Task<IActionResult> Index()
        {
            var parkingSpacePerEntryPoint = await _context.ParkingSpacePerEntryPoint
                                    .OrderBy(x => x.EntryPointName)
                                    .ThenBy(x => x.ParkingSpaceID).ToListAsync();

            return View(parkingSpacePerEntryPoint);
        }

        private void PopulateDropDownListEntryPoint()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<EntryPoint> pt = _context.EntryPoint.Where(x => x.IsActive == true).ToList();
            pt.ForEach(x => list.Add(new SelectListItem(x.EntryPointName, x.EntryPointName)));

            ViewBag.ListEntryPoint = list;
        }

        private void PopulateDropDownListParkingSpace()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<ParkingSpace> pt = new List<ParkingSpace>();
            pt = _context.ParkingSpace.Where(x => x.IsActive == true).ToList();
            pt.ForEach(x => list.Add(new SelectListItem(x.ParkingSpaceID.ToString(), x.ParkingSpaceID.ToString())));
            ViewBag.ListParkingSpace = list;
        }

        // GET: ParkingSpacePerEntryPoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpacePerEntryPoint = await _context.ParkingSpacePerEntryPoint
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parkingSpacePerEntryPoint == null)
            {
                return NotFound();
            }

            return View(parkingSpacePerEntryPoint);
        }

        // GET: ParkingSpacePerEntryPoints/Create
        public IActionResult Create()
        {
            PopulateDropDownListEntryPoint();
            PopulateDropDownListParkingSpace();
            return View();
        }

        // POST: ParkingSpacePerEntryPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EntryPointName,ParkingSpaceID")] ParkingSpacePerEntryPoint parkingSpacePerEntryPoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingSpacePerEntryPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkingSpacePerEntryPoint);
        }

        // GET: ParkingSpacePerEntryPoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpacePerEntryPoint = await _context.ParkingSpacePerEntryPoint.FindAsync(id);
            if (parkingSpacePerEntryPoint == null)
            {
                return NotFound();
            }
            PopulateDropDownListEntryPoint();
            PopulateDropDownListParkingSpace();
            return View(parkingSpacePerEntryPoint);
        }

        // POST: ParkingSpacePerEntryPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EntryPointName,ParkingSpaceID")] ParkingSpacePerEntryPoint parkingSpacePerEntryPoint)
        {
            if (id != parkingSpacePerEntryPoint.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingSpacePerEntryPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingSpacePerEntryPointExists(parkingSpacePerEntryPoint.ID))
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
            return View(parkingSpacePerEntryPoint);
        }

        // GET: ParkingSpacePerEntryPoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpacePerEntryPoint = await _context.ParkingSpacePerEntryPoint
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parkingSpacePerEntryPoint == null)
            {
                return NotFound();
            }

            return View(parkingSpacePerEntryPoint);
        }

        // POST: ParkingSpacePerEntryPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkingSpacePerEntryPoint = await _context.ParkingSpacePerEntryPoint.FindAsync(id);
            _context.ParkingSpacePerEntryPoint.Remove(parkingSpacePerEntryPoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingSpacePerEntryPointExists(int id)
        {
            return _context.ParkingSpacePerEntryPoint.Any(e => e.ID == id);
        }
    }
}
