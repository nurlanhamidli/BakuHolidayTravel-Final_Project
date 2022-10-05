using bht_demo.DAL;
using bht_demo.Extentions;
using bht_demo.Helpers;
using bht_demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace bht_demo.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class TourController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        public TourController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var tour = _context.Tours.Include(t => t.Category).OrderByDescending(b => b.Id).ToPagedList(page, 10);
            return View(tour);
        }
        public IActionResult Create()
        {
            ViewBag.Category = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tours tour)
        {
            ViewBag.Category = _context.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!tour.ImgFile.IsImage() && !tour.HeroImgFile.IsImage())
            {
                ModelState.AddModelError("ImgFile", "Şəkilin formatı düzgün deyil !");
                ModelState.AddModelError("HeroImgFile", "Şəkilin formatı düzgün deyil !");
                return View();
            }
            if (tour.ImgFile.IsImgSize(2097152) && tour.HeroImgFile.IsImgSize(2097152))
            {
                ModelState.AddModelError("ImgFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                ModelState.AddModelError("HeroImgFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                return View();
            }

            string fileName = await tour.ImgFile.SaveImage(_env, "uploads/tour");
            string heroFileName = await tour.HeroImgFile.SaveImage(_env, "uploads/tour");

            Tours newTour = new Tours();
            newTour.ImgUrl = fileName;
            newTour.HeroImgUrl = heroFileName;
            newTour.Title = tour.Title;
            newTour.Content = tour.Content;
            newTour.TourAbout = tour.TourAbout;
            newTour.Date = tour.Date;
            newTour.TourDate = tour.TourDate;
            newTour.TourMap = tour.TourMap;
            newTour.YoutubeLink = tour.YoutubeLink;
            newTour.Note = tour.Note;
            newTour.Country = tour.Country;
            newTour.Price = tour.Price;
            newTour.CategoryId = tour.CategoryId;


            await _context.Tours.AddAsync(newTour);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public ActionResult UploadImage(List<IFormFile> files)
        {
            var filepath = "";
            foreach (IFormFile photo in Request.Form.Files)
            {
                string serverMapPath = Path.Combine(_env.WebRootPath, "uploads/tour", photo.FileName);
                using (var stream = new FileStream(serverMapPath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                filepath = "https://localhost:44388/" + "uploads/tour/" + photo.FileName;
            }
            return Json(new { url = filepath });
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Category = _context.Categories.ToList();
            if (id == null) return NotFound();

            Tours dbTour = await _context.Tours.FindAsync(id);
            if (dbTour == null) return NotFound();

            return View(dbTour);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Tours tour)
        {
            ViewBag.Category = _context.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            Tours dbTour = await _context.Tours.FindAsync(id);
            if (dbTour == null) return NotFound();

            if (!tour.ImgFile.IsImage() && !tour.HeroImgFile.IsImage())
            {
                ModelState.AddModelError("ImgFile", "Şəkilin formatı düzgün deyil !");
                ModelState.AddModelError("HeroImgFile", "Şəkilin formatı düzgün deyil !");
                return View();
            }
            if (tour.ImgFile.IsImgSize(2097152) && tour.HeroImgFile.IsImgSize(2097152))
            {
                ModelState.AddModelError("ImgFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                ModelState.AddModelError("HeroImgFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                return View();
            }

            Helper.DeleteFile(_env, "uploads/tour", dbTour.ImgUrl);
            string fileName = await tour.ImgFile.SaveImage(_env, "uploads/tour");
            Helper.DeleteFile(_env, "uploads/tour", dbTour.HeroImgUrl);
            string heroFileName = await tour.HeroImgFile.SaveImage(_env, "uploads/tour");

            dbTour.ImgUrl = fileName;
            dbTour.HeroImgUrl = heroFileName;
            dbTour.Title = tour.Title;
            dbTour.Content = tour.Content;
            dbTour.TourAbout = tour.TourAbout;
            dbTour.Date = tour.Date;
            dbTour.TourDate = tour.TourDate;
            dbTour.TourMap = tour.TourMap;
            dbTour.YoutubeLink = tour.YoutubeLink;
            dbTour.Note = tour.Note;
            dbTour.Country = tour.Country;
            dbTour.Price = tour.Price;
            dbTour.CategoryId = tour.CategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.BlogCateId = _context.Blogs.FirstOrDefault();
            if (id == null) return NotFound();
            Tours dbTour = await _context.Tours.FindAsync(id);
            if (dbTour == null) return NotFound();

            if (dbTour.IsDeleted == false)
            {
                dbTour.IsDeleted = true;
            }
            else
            {
                dbTour.IsDeleted = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
    }
}
