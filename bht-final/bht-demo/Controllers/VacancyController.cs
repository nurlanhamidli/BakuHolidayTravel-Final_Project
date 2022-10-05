using bht_demo.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace bht_demo.Controllers
{
    public class VacancyController : Controller
    {
        private AppDbContext _context;

        public VacancyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var vacancy = _context.Vacancies.Where(v => v.IsDeleted == false).OrderByDescending(v => v.Id).ToList();
            return View(vacancy);
        }
    }
}
