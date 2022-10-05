using bht_demo.DAL;
using bht_demo.Extentions;
using bht_demo.Helpers;
using bht_demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bht_demo.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SlideController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!slider.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Şəkilin formatı düzgün deyil !");
                return View();
            }
            if (slider.ImageFile.IsImgSize(2097152))
            {
                ModelState.AddModelError("ImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                return View();
            }

            string fileName = await slider.ImageFile.SaveImage(_env, "uploads/slider");

            Slider newSlider = new Slider();
            newSlider.ImgUrl = fileName;
            newSlider.Title = slider.Title;
            newSlider.Description = slider.Description;
            newSlider.Link = slider.Link;
            newSlider.Type = slider.Type;

            await _context.Sliders.AddAsync(newSlider);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();

            return View(dbSlider);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();

            if (!slider.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Şəkilin formatı düzgün deyil !");
                return View();
            }
            if (slider.ImageFile.IsImgSize(2097152))
            {
                ModelState.AddModelError("ImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                return View();
            }
            Helper.DeleteFile(_env, "uploads/slider", dbSlider.ImgUrl);
            string fileName = await slider.ImageFile.SaveImage(_env, "uploads/slider");
            dbSlider.ImgUrl = fileName;
            dbSlider.Title = slider.Title;
            dbSlider.Description = slider.Description;  
            dbSlider.Link = slider.Link;
            dbSlider.Type = slider.Type;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if(dbSlider == null) return NotFound();

            Helper.DeleteFile(_env, "uploads/slider", dbSlider.ImgUrl);

            _context.Sliders.Remove(dbSlider);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
