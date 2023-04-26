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
    [Authorize]
    public class RechargesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public RechargesController(ApplicationDbContext DbContext, UserManager<ApplicationUser> manager)
        {
            _context = DbContext;
            _userManager = manager;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        // GET: Recharges
        public async Task<IActionResult> Index()
        {

            var user = await GetCurrentUser();
            var rechargeContext = _context.Recharges.Include(r => r.User)
                .Include(r => r.RechargePlan);

            if(! await _userManager.IsInRoleAsync(user, "Admin")) {
                var rechargeContextUser = _context.Recharges.Include(r => r.User)
                .Include(r => r.RechargePlan).Where(r => r.User == user);
                return View(await rechargeContextUser.ToListAsync());

            }

            return View(await rechargeContext.ToListAsync());
        }

        // GET: Recharges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recharges == null)
            {
                return NotFound();
            }

            var recharge = await _context.Recharges
                .Include(r => r.User)
                .Include(r => r.RechargePlan)
                .FirstOrDefaultAsync(m => m.RechargeId == id);
            if (recharge == null)
            {
                return NotFound();
            }

            return View(recharge);
        }

        // GET: Recharges/Create
        public async Task<IActionResult> Create(int? id)
        {
            //Console.WriteLine((await GetCurrentUser()).ToJson());
           
            ViewData["RechargePlan"] = _context.RechargePlans.Where(plan => plan.RechargePlanId == id).FirstOrDefault();

            if (ViewData["RechargePlan"] == null) return NotFound();

            return View();
        }

        // POST: Recharges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("mobileNumber")] Recharge recharge)
        {

            var rechargePlan = _context.RechargePlans.Where(plan => plan.RechargePlanId == id).First();

            if (id == null || rechargePlan == null)
            {
                return NotFound();
            }

            recharge.RechargePlanId = (int)id;
            recharge.RechargePlan = rechargePlan;
            recharge.UserId = (await GetCurrentUser()).Id;
            recharge.RechargedOn = DateTime.Now;
            recharge.ValidTill = DateTime.Now.AddDays(rechargePlan.Validity);

            Console.WriteLine(recharge.ToJson());

            if(recharge.mobileNumber.Length < 10)
            {
                ViewData["RechargePlan"] = rechargePlan;
                return View(recharge);
            }


            _context.Add(recharge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        [Authorize(Roles = "Admin")]
        // GET: Recharges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recharges == null)
            {
                return NotFound();
            }

            var recharge = await _context.Recharges
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RechargeId == id);
            if (recharge == null)
            {
                return NotFound();
            }

            return View(recharge);
        }

        // POST: Recharges/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recharges == null)
            {
                return Problem("Entity set 'RechargeContext.Recharges'  is null.");
            }
            var recharge = await _context.Recharges.FindAsync(id);
            if (recharge != null)
            {
                _context.Recharges.Remove(recharge);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechargeExists(int id)
        {
          return (_context.Recharges?.Any(e => e.RechargeId == id)).GetValueOrDefault();
        }
    }
}
