using bht_demo.Areas.AdminPanel.ViewModels;
using bht_demo.DAL;
using bht_demo.Helpers;
using bht_demo.Models;
using bht_demo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bht_demo.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _emailService = emailService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLogin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser admin = await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == adminLogin.Username.ToUpper() && x.IsAdmin == true);
            if (admin == null)
            {
                ModelState.AddModelError("", "Ad və ya Şifrə yanlışdır!!!");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(admin, adminLogin.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Ad və ya Şifrə yanlışdır!!!");
                return View();
            }

            return RedirectToAction("index", "dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
        public IActionResult Admins()
        {

            if (!User.IsInRole("SuperAdmin"))
            {
                return RedirectToAction("index", "error");
            }
            return View(_context.AppUsers.ToList());
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult CreateAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(AdminViewModel adminView)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser admin = await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == adminView.Email);
            if (admin != null)
            {
                ModelState.AddModelError("Email", "Bu Email hal hazırda istifadə olunur");
                return View();
            }
            admin = new AppUser
            {
                Name = adminView.Name,
                Surname = adminView.Surname,
                Email = adminView.Email,
                UserName = adminView.Email,
                PhoneNumber = adminView.PhoneNumber,
                IsAdmin = true
            };
            var result = await _userManager.CreateAsync(admin, adminView.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(admin, "Admin");
            return RedirectToAction("index", "dashboard");
        }
        //public async Task<IActionResult> CreateSuperAdmin()
        //{
        //    AppUser superadmin = new AppUser
        //    {
        //        UserName = "hnurlan",
        //        IsAdmin = true,
        //        Email = "nurlan12@gmail.com",
        //        PhoneNumber = "0777672277"
        //    };

        //    var result = await _userManager.CreateAsync(superadmin, "Admin2277");
        //    await _userManager.AddToRoleAsync(superadmin, "SuperAdmin");
        //    return Ok(result);
        //}
        //public async Task CreateRole()
        //{
        //    foreach (var item in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(item.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
        //        }
        //    }
        //}
    }
}
