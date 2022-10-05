using bht_demo.DAL;
using bht_demo.Extentions;
using bht_demo.Helpers;
using bht_demo.Models;
using bht_demo.Areas.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;

namespace bht_demo.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class BlogController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Index(int? id)
        {           
            BlogViewModel blogVM = new BlogViewModel();
            blogVM.Blogs = _context.Blogs.Include(b => b.BlogCategory).Where(b => b.BlogCategoryId == id).ToList();
            blogVM.BlogCategory = _context.BlogCategories.Find(id);
            blogVM.Blog = _context.Blogs.FirstOrDefault();
            blogVM.BlogCategories = _context.BlogCategories.ToList();
            return View(blogVM);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Create()
        {
            ViewBag.BlogCategory = _context.BlogCategories.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Create(Blogs blog)
        {
            ViewBag.BlogCategory = _context.BlogCategories.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!blog.ImageFile.IsImage() && !blog.DetailImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Şəkilin formatı düzgün deyil !");
                ModelState.AddModelError("DetailImageFile", "Şəkilin formatı düzgün deyil !");
                return View();
            }
            if (blog.ImageFile.IsImgSize(2097152) && blog.DetailImageFile.IsImgSize(2097152))
            {
                ModelState.AddModelError("ImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                ModelState.AddModelError("DetailImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                return View();
            }

            string fileName = await blog.ImageFile.SaveImage(_env, "uploads/blog");
            string detailFileName = await blog.DetailImageFile.SaveImage(_env, "uploads/blog");

            Blogs newBlog = new Blogs();
            newBlog.ImgUrl = fileName;
            newBlog.DetailImgUrl = detailFileName;
            newBlog.Title = blog.Title;
            newBlog.ShortText = blog.ShortText;
            newBlog.Date = blog.Date;
            newBlog.Content = blog.Content;
            newBlog.BlogCategoryId = blog.BlogCategoryId;


            await _context.Blogs.AddAsync(newBlog);
            await _context.SaveChangesAsync();

            return RedirectToAction("index", new {id = blog.BlogCategoryId});
        }
        public ActionResult UploadImage(List<IFormFile> files)
        {
            var filepath = "";
            foreach (IFormFile photo in Request.Form.Files)
            {
                string serverMapPath = Path.Combine(_env.WebRootPath, "uploads/blog", photo.FileName);
                using(var stream = new FileStream(serverMapPath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                filepath = "https://localhost:44388/" + "uploads/blog/" + photo.FileName;
            }
            return Json(new { url = filepath });
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.BlogCategory = _context.BlogCategories.ToList();
            if (id == null) return NotFound();

            Blogs dbBlog = await _context.Blogs.FindAsync(id);
            if (dbBlog == null) return NotFound();

            return View(dbBlog);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]

        public async Task<IActionResult> Update(int? id, Blogs blog)
        {
            ViewBag.BlogCategory = _context.BlogCategories.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            Blogs dbBlog = await _context.Blogs.FindAsync(id);
            if (dbBlog == null) return NotFound();

            if (!blog.ImageFile.IsImage() && !blog.DetailImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Şəkilin formatı düzgün deyil !");
                ModelState.AddModelError("DetailImageFile", "Şəkilin formatı düzgün deyil !");
                return View();
            }
            if (blog.ImageFile.IsImgSize(2097152) && blog.DetailImageFile.IsImgSize(2097152))
            {
                ModelState.AddModelError("ImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                ModelState.AddModelError("DetailImageFile", "Şəkilin ölçüsü böyükdür (max:2mb)");
                return View();
            }
            Helper.DeleteFile(_env, "uploads/blog", dbBlog.ImgUrl);
            string fileName = await blog.ImageFile.SaveImage(_env, "uploads/blog");
            Helper.DeleteFile(_env, "uploads/blog", dbBlog.DetailImgUrl);
            string detailFileName = await blog.DetailImageFile.SaveImage(_env, "uploads/blog");
            dbBlog.ImgUrl = fileName;
            dbBlog.DetailImgUrl = detailFileName;
            dbBlog.Title = blog.Title;
            dbBlog.ShortText = blog.ShortText;
            dbBlog.Date = blog.Date;
            dbBlog.Content = blog.Content;
            dbBlog.BlogCategoryId = blog.BlogCategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction("index", new { id = blog.BlogCategoryId });
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.BlogCateId = _context.Blogs.FirstOrDefault();
            if (id == null) return NotFound();
            Blogs dbBlog = await _context.Blogs.FindAsync(id);
            if (dbBlog == null) return NotFound();

            Helper.DeleteFile(_env, "uploads/blog", dbBlog.ImgUrl);
            Helper.DeleteFile(_env, "uploads/blog", dbBlog.DetailImgUrl);

            _context.Blogs.Remove(dbBlog);

            await _context.SaveChangesAsync();
            return RedirectToAction("index", new { id = ViewBag.BlogCateId.BlogCategoryId });
        }
    }
}
