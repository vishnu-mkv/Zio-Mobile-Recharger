using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mobile_recharger.Data;
using mobile_recharger.Models;
using NuGet.Protocol;

namespace mobile_recharger.Controllers
{
    public class RechargePlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RechargePlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RechargePlans
        public async Task<IActionResult> Index()
        {
            var rechargeContext = _context.RechargePlans.Include(r => r.Category);
            return View(await rechargeContext.ToListAsync());
        }

        // GET: RechargePlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RechargePlans == null)
            {
                return NotFound();
            }


            var rechargePlan = await _context.RechargePlans
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.RechargePlanId == id);
            if (rechargePlan == null)
            {
                return NotFound();
            }

            return View(rechargePlan);
        }

        // GET: RechargePlans/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "CategoryId", "Name");

            return View();
        }

        // POST: RechargePlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Price,Validity,CategoryId")] RechargePlan rechargePlan)
        {
            if (ModelState.IsValid)
            {

                _context.Add(rechargePlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "CategoryId", "Name", rechargePlan.CategoryId);
            return View(rechargePlan);
        }

        // GET: RechargePlans/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RechargePlans == null)
            {
                return NotFound();
            }

            var rechargePlan = await _context.RechargePlans.FindAsync(id);
            if (rechargePlan == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "CategoryId", "Name", rechargePlan.CategoryId);
          
            return View(rechargePlan);
        }

        // POST: RechargePlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("RechargePlanId,Price,Validity,CategoryId")] RechargePlan rechargePlan)
        {
            if (id != rechargePlan.RechargePlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rechargePlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RechargePlanExists(rechargePlan.RechargePlanId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "CategoryId", "Name", rechargePlan.CategoryId);
            return View(rechargePlan);
        }

        // GET: RechargePlans/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RechargePlans == null)
            {
                return NotFound();
            }

            var rechargePlan = await _context.RechargePlans
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.RechargePlanId == id);
            if (rechargePlan == null)
            {
                return NotFound();
            }

            return View(rechargePlan);
        }

        // POST: RechargePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RechargePlans == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RechargePlans'  is null.");
            }
            var rechargePlan = await _context.RechargePlans.FindAsync(id);
            if (rechargePlan != null)
            {
                _context.RechargePlans.Remove(rechargePlan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechargePlanExists(int id)
        {
          return (_context.RechargePlans?.Any(e => e.RechargePlanId == id)).GetValueOrDefault();
        }
    }
}
