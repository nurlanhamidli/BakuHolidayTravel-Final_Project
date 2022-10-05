using bht_demo.DAL;
using bht_demo.Models;
using bht_demo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace bht_demo.Controllers
{
    public class AboutController : Controller
    {
        private AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AboutVM aboutVM = new AboutVM();
            aboutVM.ServiceAndAbouts = _context.ServiceAndAbouts.Where(s => s.IsDeleted == false && s.Type == 1).ToList();
            aboutVM.BlogCategory = _context.BlogCategories.Where(c=>c.Id == 3).FirstOrDefault();
            return View(aboutVM);
        }
        public IActionResult Team()
        {
            return View();
        }
    }
}
