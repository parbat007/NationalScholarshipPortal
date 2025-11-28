using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScholarshipPortal.Data;
using ScholarshipPortal.Models;

namespace ScholarshipPortal.Controllers
{
    public class MinistryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MinistryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ======================
        // REGISTER (GET)
        // ======================
        [HttpGet]
        public IActionResult Register()
        {
            return View();   // Views/Ministry/Register.cshtml
        }

        // ======================
        // REGISTER (POST)
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MinistryUser model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _context.MinistryUsers.AnyAsync(m => m.Email == model.Email))
            {
                ModelState.AddModelError("", "Email already registered.");
                return View(model);
            }

            _context.MinistryUsers.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Registration successful!";
            return RedirectToAction(nameof(RegistrationSuccess));
        }

        // ======================
        // REGISTRATION SUCCESS
        // ======================
        [HttpGet]
        public IActionResult RegistrationSuccess()
        {
            if (TempData["SuccessMessage"] == null)
                return RedirectToAction(nameof(Register));

            return View();  // Views/Ministry/RegistrationSuccess.cshtml
        }

        // ======================
        // LOGIN (GET)
        // ======================
        [HttpGet]
        public IActionResult Login()
        {
            return View();  // Views/Ministry/Login.cshtml
        }

        // ======================
        // LOGIN (POST)
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.MinistryUsers
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid email or password.";
                return View();
            }

            TempData["MinistryEmail"] = user.Email;
            TempData.Keep("MinistryEmail");

            return RedirectToAction(nameof(Dashboard));
        }

        // ======================
        // LOGOUT
        // ======================
        [HttpGet]
        public IActionResult Logout()
        {
            TempData.Remove("MinistryEmail");
            TempData["SuccessMessage"] = "Logged out successfully.";
            return RedirectToAction(nameof(Login));
        }

        // ======================
        // DASHBOARD
        // ======================
        [HttpGet]
        public IActionResult Dashboard()
        {
            string email = TempData.Peek("MinistryEmail")?.ToString();
            if (email == null)
                return RedirectToAction(nameof(Login));

            TempData.Keep("MinistryEmail");

            var model = new MinistryDashboardViewModel
            {
                MinistryEmail = email,
                TotalApplications = _context.ScholarshipApplications.Count(),
                TotalInstitutes = _context.Institutes.Count(),
                TotalStudents = _context.RegisteredUsers.Count()
            };

            return View(model);  // Views/Ministry/Dashboard.cshtml
        }

        // ======================
        // STUDENT APPLICATIONS
        // ======================
        [HttpGet]
        public async Task<IActionResult> StudentApplications(string q = null)
        {
            string email = TempData.Peek("MinistryEmail")?.ToString();
            if (email == null)
                return RedirectToAction(nameof(Login));

            TempData.Keep("MinistryEmail");

            var query = _context.ScholarshipApplications.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(a =>
                    EF.Functions.Like(a.UserId, $"%{q}%") ||
                    EF.Functions.Like(a.SchemeName, $"%{q}%"));
            }

            var list = await query
                .OrderByDescending(a => a.ApplicationId)
                .ToListAsync();

            return View(list);  // Views/Ministry/StudentApplications.cshtml
        }

        // ======================
        // INSTITUTE APPLICATIONS
        // ======================
        [HttpGet]
        public async Task<IActionResult> InstituteApplications(string q = null)
        {
            string email = TempData.Peek("MinistryEmail")?.ToString();
            if (email == null)
                return RedirectToAction(nameof(Login));

            TempData.Keep("MinistryEmail");

            var query = _context.Institutes.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(i =>
                    EF.Functions.Like(i.InstituteName, $"%{q}%"));
            }

            var list = await query.ToListAsync();

            return View(list);  // Views/Ministry/InstituteApplications.cshtml
        }

        // ========================================================
        // MINISTRY APPROVAL — STUDENT
        // ========================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessStudent(int id, string decision, string remarks)
        {
            var app = await _context.ScholarshipApplications.FindAsync(id);
            if (app == null) return NotFound();

            app.MinistryStatus = decision == "Approve" ? "Approved" : "Rejected";
            app.MinistryRemarks = remarks;
            app.MinistryActionDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Student Application {decision} Successfully!";
            return RedirectToAction(nameof(StudentApplications));
        }

        // ========================================================
        // MINISTRY APPROVAL — INSTITUTE (NEW)
        // ========================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessInstitute(int id, string decision, string remarks)
        {
            var inst = await _context.Institutes.FirstOrDefaultAsync(i => i.InstituteId == id);
            if (inst == null) return NotFound();

            inst.MinistryStatus = decision == "Approve" ? "Approved" : "Rejected";
            inst.MinistryRemarks = remarks;
            inst.MinistryActionDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Institute {decision} Successfully!";
            return RedirectToAction(nameof(InstituteApplications));
        }

        // ======================
        // VIEW STUDENT APPLICATION (FULL DETAILS)
        // ======================
        [HttpGet]
        public async Task<IActionResult> ViewStudent(int id)
        {
            string email = TempData.Peek("MinistryEmail")?.ToString();
            if (email == null)
                return RedirectToAction(nameof(Login));

            TempData.Keep("MinistryEmail");

            var app = await _context.ScholarshipApplications
                        .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (app == null) return NotFound();

            return View(app);   // Views/Ministry/ViewStudent.cshtml
        }


        // ======================
        // VIEW INSTITUTE DETAILS (FULL DETAILS)
        // ======================
        [HttpGet]
        public async Task<IActionResult> ViewInstitute(int id)
        {
            string email = TempData.Peek("MinistryEmail")?.ToString();
            if (email == null)
                return RedirectToAction(nameof(Login));

            TempData.Keep("MinistryEmail");

            var inst = await _context.Institutes
                        .FirstOrDefaultAsync(i => i.InstituteId == id);

            if (inst == null) return NotFound();

            return View(inst);   // Views/Ministry/ViewInstitute.cshtml
        }

    }
}