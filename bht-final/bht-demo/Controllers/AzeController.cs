using bht_demo.DAL;
using bht_demo.Models;
using bht_demo.Services;
using bht_demo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace bht_demo.Controllers
{
    public class AzeController : Controller
    {
        private AppDbContext _context;

        public AzeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            AzeVM azeVM = new AzeVM();
            azeVM.sliders = _context.Sliders.ToList();
            azeVM.Tours = _context.Tours.Where(t => t.Country == "0" && t.IsDeleted == false).OrderByDescending(b => b.Id).ToList();

            return View(azeVM);
        }
        //public IActionResult Detail(int? id)
        //{
        //    if (id == null) return NotFound();
        //    AzeVM azeVM = new AzeVM();
        //    azeVM.Tour = _context.Tours.Find(id);
        //    azeVM.Tours = _context.Tours.Where(c => c.IsDeleted == false).Include(t => t.Category).OrderByDescending(b => b.Id).ToList();

        //    return View(azeVM);
        //}
    }
}
