using bht_demo.DAL;
using bht_demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace bht_demo.Controllers
{
    public class ContactController : Controller
    {
        private AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactUs contactUs)
        {

            ContactUs newMessage = new ContactUs();
            newMessage.Name = contactUs.Name;
            newMessage.Email = contactUs.Email;
            newMessage.Surname = contactUs.Surname;
            newMessage.PhoneNumber = contactUs.PhoneNumber;
            newMessage.Message = contactUs.Message;

            await _context.ContactUs.AddAsync(newMessage);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
    }
}
