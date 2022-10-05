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
    public class VacancyController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        public VacancyController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var vacancy = _context.Vacancies.OrderByDescending(b => b.Id).ToPagedList(page, 5);
            return View(vacancy);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vacancy vacancy)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Vacancy newVacancy = new Vacancy();
            newVacancy.Position = vacancy.Position;
            newVacancy.City = vacancy.City;
            newVacancy.Department = vacancy.Department;
            newVacancy.EndDate = vacancy.EndDate;
            newVacancy.Content = vacancy.Content;


            await _context.Vacancies.AddAsync(newVacancy);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Vacancy dbVacancy = await _context.Vacancies.FindAsync(id);
            if (dbVacancy == null) return NotFound();

            return View(dbVacancy);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Vacancy vacancy)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Vacancy dbVacancy = await _context.Vacancies.FindAsync(id);
            if (dbVacancy == null) return NotFound();
            dbVacancy.Position = vacancy.Position;
            dbVacancy.City = vacancy.City;
            dbVacancy.Department = vacancy.Department;
            dbVacancy.EndDate = vacancy.EndDate;
            dbVacancy.Content = vacancy.Content;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Vacancy dbVacancy = await _context.Vacancies.FindAsync(id);
            if (dbVacancy == null) return NotFound();

            if (dbVacancy.IsDeleted == false)
            {
                dbVacancy.IsDeleted = true;
            }
            else
            {
                dbVacancy.IsDeleted = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
