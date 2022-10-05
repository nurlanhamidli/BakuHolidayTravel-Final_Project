using bht_demo.DAL;
using bht_demo.Models;
using bht_demo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace bht_demo.Controllers
{
    public class TourController : Controller
    {
        private AppDbContext _context;

        public TourController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            TourVM tourVM = new TourVM();
            tourVM.Categories = _context.Categories.Where(c => c.IsDeleted == false).ToList();
            if (id == null)
            {
                tourVM.Tours = _context.Tours.Where(c => c.IsDeleted == false).Include(t => t.Category).OrderByDescending(b => b.Id).ToList();
            }
            if (id != null)
            {
                tourVM.Tours = _context.Tours.Where(c => c.IsDeleted == false && c.CategoryId == id).Include(t => t.Category).OrderByDescending(b => b.Id).ToList();
            }
            return View(tourVM);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            TourVM tourDetailVM = new TourVM();
            tourDetailVM.Tour = _context.Tours.Find(id);
            tourDetailVM.Tours = _context.Tours.Where(c => c.IsDeleted == false).Include(t => t.Category).OrderByDescending(b => b.Id).Take(8).ToList();

            return View(tourDetailVM);
        }
        public IActionResult Search(string search)
        {
            List<Tours> tours = _context.Tours.Include(t => t.Category).Where(t => t.Title.ToLower().Contains(search.ToLower()) || t.Category.Name.ToLower().Contains(search.ToLower())).OrderByDescending(t=>t.Id).Take(10).ToList();
            return PartialView("_Search", tours);
        }
    }
}
