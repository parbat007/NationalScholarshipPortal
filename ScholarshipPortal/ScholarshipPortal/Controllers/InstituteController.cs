using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScholarshipPortal.Data;
using ScholarshipPortal.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ScholarshipPortal.Controllers
{
    public class InstituteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstituteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LOGIN (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // LOGIN (POST)
        [HttpPost]
        public async Task<IActionResult> Login(InstituteLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var institute = await _context.Institutes
                .FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if (institute == null)
            {
                ModelState.AddModelError("", "Invalid login credentials");
                return View(model);
            }

            HttpContext.Session.SetString("InstituteEmail", institute.Email);
            HttpContext.Session.SetInt32("InstituteId", institute.InstituteId);

            ViewBag.InstituteName = institute.InstituteName;
            return RedirectToAction("Dashboard");
        }

        // DASHBOARD
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("InstituteId") == null)
                return RedirectToAction("Login");

            var name = HttpContext.Session.GetString("InstituteEmail");
            ViewBag.InstituteName = name;
            return View();
        }

        // REGISTER (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // REGISTER (POST)
        [HttpPost]
        public async Task<IActionResult> Register(InstituteRegistration model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Institutes.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("RegistrationSuccess");
        }

        // SUCCESS PAGE
        public IActionResult RegistrationSuccess()
        {
            return View();
        }

        // VIEW PROFILE
        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            int? instituteId = HttpContext.Session.GetInt32("InstituteId");
            if (instituteId == null)
                return RedirectToAction("Login");

            var institute = await _context.Institutes.FirstOrDefaultAsync(x => x.InstituteId == instituteId);
            return View(institute);
        }

        // EDIT PROFILE (GET)
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            int? instituteId = HttpContext.Session.GetInt32("InstituteId");
            if (instituteId == null)
                return RedirectToAction("Login");

            var institute = await _context.Institutes.FirstOrDefaultAsync(x => x.InstituteId == instituteId);
            return View(institute);
        }

        // EDIT PROFILE (POST)
        [HttpPost]
        public async Task<IActionResult> EditProfile(InstituteRegistration model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Institutes.Update(model);
            await _context.SaveChangesAsync();

            TempData["ProfileUpdated"] = "Profile updated successfully!";
            return RedirectToAction("ViewProfile");
        }

        // LOGOUT 
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
