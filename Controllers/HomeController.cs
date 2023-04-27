using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mobile_recharger.Data;
using mobile_recharger.Models;
using System.Diagnostics;

namespace mobile_recharger.Controllers
{

    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Recharge> Recharges { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUser();
            var categories = _context.Categorys.Include(c => c.RechargePlans);
            var recharges = _context.Recharges.Include(r => r.User)
                .Include(r => r.RechargePlan)
                .ThenInclude(p => p.Category)
                .Where(r => r.User == user)
                .Where(r => r.ValidTill > DateTime.Now);

            HomeViewModel data = new HomeViewModel(){ Categories=  categories.ToList(), Recharges= recharges.ToList()};
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}