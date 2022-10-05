using bht_demo.DAL;
using bht_demo.Extentions;
using bht_demo.Helpers;
using bht_demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace bht_demo.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExistName = _context.Categories.Any(c=>c.Name.ToLower() == category.Name.ToLower());
            if (isExistName)
            {
                ModelState.AddModelError("Name", "Eyni Adli Kateqoriya movcuddur!");
                return View();
            }

            Category newCategory = new Category();
            newCategory.Name = category.Name;

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();

            return View(dbCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExistName = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (isExistName)
            {
                ModelState.AddModelError("Name", "Eyni Adli Kateqoriya movcuddur!");
                return View();
            }
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();
            dbCategory.Name = category.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();

            if (dbCategory.IsDeleted == false)
            {
                dbCategory.IsDeleted = true;
            }
            else
            {
                dbCategory.IsDeleted = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
