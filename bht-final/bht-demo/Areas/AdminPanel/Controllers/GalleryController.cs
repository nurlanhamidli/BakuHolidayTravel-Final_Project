using bht_demo.DAL;
using bht_demo.Extentions;
using bht_demo.Helpers;
using bht_demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace bht_demo.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class GalleryController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        public GalleryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var gallery = _context.Galleries.OrderByDescending(b => b.Id).ToPagedList(page, 5);
            return View(gallery);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!gallery.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Şəkilin formatı düzgün deyil !");
                return View();
            }
            if (gallery.ImageFile.IsImgSize(2097152))
            {
                ModelState.AddModelError("ImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                return View();
            }

            string fileName = await gallery.ImageFile.SaveImage(_env, "uploads/gallery");

            Gallery newGallery = new Gallery();
            newGallery.ImgUrl = fileName;
            newGallery.Column = gallery.Column;

            await _context.Galleries.AddAsync(newGallery);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Gallery dbGallery = await _context.Galleries.FindAsync(id);
            if (dbGallery == null) return NotFound();

            return View(dbGallery);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Gallery dbGallery = await _context.Galleries.FindAsync(id);
            if (dbGallery == null) return NotFound();

            if (!gallery.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Şəkilin formatı düzgün deyil !");
                return View();
            }
            if (gallery.ImageFile.IsImgSize(2097152))
            {
                ModelState.AddModelError("ImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                return View();
            }
            Helper.DeleteFile(_env, "uploads/gallery", dbGallery.ImgUrl);
            string fileName = await gallery.ImageFile.SaveImage(_env, "uploads/gallery");
            dbGallery.ImgUrl = fileName;
            dbGallery.Column = gallery.Column;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Gallery dbGallery = await _context.Galleries.FindAsync(id);
            if (dbGallery == null) return NotFound();

            if (dbGallery.IsDeleted == false)
            {
                dbGallery.IsDeleted = true;
            }
            else
            {
                dbGallery.IsDeleted = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
