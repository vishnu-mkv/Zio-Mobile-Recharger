using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mobile_recharger.Data;
using mobile_recharger.Models;
using System.Diagnostics;

namespace mobile_recharger.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categorys.Include(c => c.RechargePlans);
            return View(categories.ToList());
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