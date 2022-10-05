using bht_demo.DAL;
using bht_demo.Extentions;
using bht_demo.Helpers;
using bht_demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace bht_demo.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PagesController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        public PagesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var pageList = _context.ServiceAndAbouts.OrderByDescending(b => b.Id).ToPagedList(page, 5);
            return View(pageList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceAndAbout serviceAndAbout)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!serviceAndAbout.IconFile.IsImage() && !serviceAndAbout.HeroImgFile.IsImage())
            {
                ModelState.AddModelError("IconFile", "Şəkilin formatı düzgün deyil !");
                ModelState.AddModelError("HeroImgUrl", "Şəkilin formatı düzgün deyil !");
                return View();
            }
            if (serviceAndAbout.IconFile.IsImgSize(2097152) && serviceAndAbout.HeroImgFile.IsImgSize(2097152))
            {
                ModelState.AddModelError("IconFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                ModelState.AddModelError("HeroImgFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                return View();
            }
            bool isExistName = _context.ServiceAndAbouts.Any(c => c.Name.ToLower() == serviceAndAbout.Name.ToLower());
            if (isExistName)
            {
                ModelState.AddModelError("Name", "Eyni Adli Kateqoriya movcuddur!");
                return View();
            }

            string fileName = await serviceAndAbout.IconFile.SaveImage(_env, "uploads/page");
            string heroFileName = await serviceAndAbout.HeroImgFile.SaveImage(_env, "uploads/page");

            ServiceAndAbout newPage = new ServiceAndAbout();
            newPage.Icon = fileName;
            newPage.HeroImgUrl = heroFileName;
            newPage.Name = serviceAndAbout.Name;
            newPage.Title = serviceAndAbout.Title;
            newPage.Desc = serviceAndAbout.Desc;
            newPage.Content = serviceAndAbout.Content;
            newPage.Type = serviceAndAbout.Type;


            await _context.ServiceAndAbouts.AddAsync(newPage);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public ActionResult UploadImage(List<IFormFile> files)
        {
            var filepath = "";
            foreach (IFormFile photo in Request.Form.Files)
            {
                string serverMapPath = Path.Combine(_env.WebRootPath, "uploads/page", photo.FileName);
                using (var stream = new FileStream(serverMapPath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                filepath = "https://localhost:44388/" + "uploads/page/" + photo.FileName;
            }
            return Json(new { url = filepath });
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            ServiceAndAbout dbpage = await _context.ServiceAndAbouts.FindAsync(id);
            if (dbpage == null) return NotFound();

            return View(dbpage);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, ServiceAndAbout serviceAndAbout)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!serviceAndAbout.IconFile.IsImage() && !serviceAndAbout.HeroImgFile.IsImage())
            {
                ModelState.AddModelError("IconFile", "Şəkilin formatı düzgün deyil !");
                ModelState.AddModelError("HeroImgUrl", "Şəkilin formatı düzgün deyil !");
                return View();
            }
            if (serviceAndAbout.IconFile.IsImgSize(2097152) && serviceAndAbout.HeroImgFile.IsImgSize(2097152))
            {
                ModelState.AddModelError("IconFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                ModelState.AddModelError("HeroImgFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                return View();
            }
            bool isExistName = _context.ServiceAndAbouts.Any(c => c.Name.ToLower() == serviceAndAbout.Name.ToLower());
            if (isExistName)
            {
                ModelState.AddModelError("Name", "Eyni Adli Kateqoriya movcuddur!");
                return View();
            }
            ServiceAndAbout dbPage = await _context.ServiceAndAbouts.FindAsync(id);
            if (dbPage == null) return NotFound();

            Helper.DeleteFile(_env, "uploads/page", dbPage.Icon);
            string fileName = await serviceAndAbout.IconFile.SaveImage(_env, "uploads/tour");
            Helper.DeleteFile(_env, "uploads/page", dbPage.HeroImgUrl);
            string heroFileName = await serviceAndAbout.HeroImgFile.SaveImage(_env, "uploads/tour");

            dbPage.Icon = fileName;
            dbPage.HeroImgUrl = heroFileName;
            dbPage.Name = serviceAndAbout.Name;
            dbPage.Title = serviceAndAbout.Title;
            dbPage.Desc = serviceAndAbout.Desc;
            dbPage.Content = serviceAndAbout.Content;
            dbPage.Type = serviceAndAbout.Type;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            ServiceAndAbout dbPage = await _context.ServiceAndAbouts.FindAsync(id);
            if (dbPage == null) return NotFound();

            if (dbPage.IsDeleted == false)
            {
                dbPage.IsDeleted = true;
            }
            else
            {
                dbPage.IsDeleted = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
