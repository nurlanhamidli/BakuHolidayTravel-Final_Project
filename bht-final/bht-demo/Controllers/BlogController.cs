using bht_demo.DAL;
using bht_demo.Models;
using bht_demo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace bht_demo.Controllers
{
    public class BlogController : Controller
    {
        private AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id, int page=1)
        {

            BlogVM blogVM = new BlogVM();
            blogVM.Blogs = _context.Blogs.Where(b => b.BlogCategoryId == id).OrderByDescending(b => b.Id).ToList();
            blogVM.BlogCategory = _context.BlogCategories.Find(id);
            return View(blogVM);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();

            return View(_context.Blogs.Find(id));
        }
    }
}
