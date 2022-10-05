using bht_demo.DAL;
using bht_demo.Helpers;
using bht_demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace bht_demo.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ContactUsController : Controller
    {
        private AppDbContext _context;
        public ContactUsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.ContactUs.OrderByDescending(m => m.Id).ToList());
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            ContactUs contactUs = await _context.ContactUs.FindAsync(id);
            if (contactUs == null) return NotFound();

            _context.ContactUs.Remove(contactUs);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            ContactUs contactUs = await _context.ContactUs.FirstOrDefaultAsync(s => s.Id == id);
            if (contactUs == null) return NotFound();
            return View(contactUs);
        }
    }
}
