using bht_demo.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace bht_demo.Controllers
{
    public class GalleryController : Controller
    {
        private AppDbContext _context;

        public GalleryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var gallery = _context.Galleries.Where(g=>g.IsDeleted == false).OrderByDescending(g=>g.Id).ToList();
            return View(gallery);
        }
    }
}
