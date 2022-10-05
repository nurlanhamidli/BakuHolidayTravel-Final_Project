using bht_demo.DAL;
using bht_demo.Models;
using bht_demo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace bht_demo.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.settings = _context.Settings.FirstOrDefault();
            homeVM.sliders = _context.Sliders.ToList();
            homeVM.certificates = _context.Certificates.ToList();
            homeVM.places = _context.Places.ToList();
            homeVM.ForeignTours = _context.Tours.Where(c => c.IsDeleted == false && c.Country == "1" && c.Category.Name.ToLower() != "kruiz".ToLower() && c.Category.Name.ToLower() != "qrup turları".ToLower() && c.Category.Name.ToLower() != "xüsusi təkliflər".ToLower()).Include(t => t.Category).OrderByDescending(b => b.Id).Take(4).ToList();
            homeVM.QrupTours = _context.Tours.Where(c => c.IsDeleted == false &&  c.Category.Name.ToLower() == "qrup turları".ToLower()).Include(t => t.Category).OrderByDescending(b => b.Id).Take(4).ToList();
            homeVM.KruizTours = _context.Tours.Where(c => c.IsDeleted == false && c.Category.Name.ToLower() == "kruiz".ToLower()).Include(t => t.Category).OrderByDescending(b => b.Id).ToList();
            homeVM.FeaturedTours = _context.Tours.Where(c => c.IsDeleted == false && c.Category.Name.ToLower() == "xüsusi təkliflər".ToLower()).Include(t => t.Category).OrderByDescending(b => b.Id).ToList();
            return View(homeVM);
        }
    }
}
