using bht_demo.DAL;
using bht_demo.Models;
using bht_demo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace bht_demo.Controllers
{
    public class ServiceController : Controller
    {
        private AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var service = _context.ServiceAndAbouts.Where(s => s.IsDeleted == false && s.Type == 0).ToList();
            return View(service);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            ServiceVM serviceVM = new ServiceVM();
            serviceVM.ContactUs = _context.ContactUs.FirstOrDefault();
            serviceVM.ServiceAndAbout = _context.ServiceAndAbouts.Find(id);

            return View(serviceVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(ContactUs contactUs)
        {

            ContactUs newMessage = new ContactUs();
            newMessage.Name = contactUs.Name;
            newMessage.Email = contactUs.Email;
            newMessage.Surname = contactUs.Surname;
            newMessage.PhoneNumber = contactUs.PhoneNumber;
            newMessage.Message = contactUs.Message;

            await _context.ContactUs.AddAsync(newMessage);
            await _context.SaveChangesAsync();

            return RedirectToAction("detail");
        }
    }
}
