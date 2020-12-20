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
    public class EntryPointsController : Controller
    {
        private readonly ParkingContext _context;

        public EntryPointsController(ParkingContext context)
        {
            _context = context;
        }

        // GET: EntryPoints
        public async Task<IActionResult> Index()
        {
            return View(await _context.EntryPoint.ToListAsync());
        }

        // GET: EntryPoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entryPoint = await _context.EntryPoint
                .FirstOrDefaultAsync(m => m.EntryPointID == id);
            if (entryPoint == null)
            {
                return NotFound();
            }

            return View(entryPoint);
        }

        // GET: EntryPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EntryPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntryPointID,EntryPointName,Description,IsActive")] EntryPoint entryPoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entryPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entryPoint);
        }

        // GET: EntryPoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entryPoint = await _context.EntryPoint.FindAsync(id);
            if (entryPoint == null)
            {
                return NotFound();
            }
            return View(entryPoint);
        }

        // POST: EntryPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntryPointID,EntryPointName,Description,IsActive")] EntryPoint entryPoint)
        {
            if (id != entryPoint.EntryPointID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entryPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntryPointExists(entryPoint.EntryPointID))
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
            return View(entryPoint);
        }

        // GET: EntryPoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entryPoint = await _context.EntryPoint
                .FirstOrDefaultAsync(m => m.EntryPointID == id);
            if (entryPoint == null)
            {
                return NotFound();
            }

            return View(entryPoint);
        }

        // POST: EntryPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entryPoint = await _context.EntryPoint.FindAsync(id);
            _context.EntryPoint.Remove(entryPoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntryPointExists(int id)
        {
            return _context.EntryPoint.Any(e => e.EntryPointID == id);
        }
    }
}
