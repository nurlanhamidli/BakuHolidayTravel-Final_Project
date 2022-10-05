using bht_demo.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace bht_demo.Controllers
{
    public class InfoController : Controller
    {
        private AppDbContext _context;
        public InfoController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.BlogCategories.ToList());
        }
    }
}
