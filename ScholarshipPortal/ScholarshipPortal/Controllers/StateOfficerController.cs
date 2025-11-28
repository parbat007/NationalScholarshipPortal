using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ScholarshipPortal.Data;
using ScholarshipPortal.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipPortal.Controllers
{
    public class StateOfficerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 10;

        public StateOfficerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // REGISTRATION
        // =========================

        [HttpGet]
        public IActionResult Register()
        {
            return View(new StateOfficer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(StateOfficer model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool emailExists = await _context.StateUsers
                .AnyAsync(x => x.Email == model.Email);

            if (emailExists)
            {
                ModelState.AddModelError("Email", "An officer with this email already exists.");
                return View(model);
            }

            _context.StateUsers.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "State Officer account registered successfully!";
            return RedirectToAction("Login");
        }


        // =========================
        // LOGIN / LOGOUT
        // =========================

        [HttpGet]
        public IActionResult Login()
        {
            // This prevents Dashboard → Logout → Login from showing old success messages
            TempData["SuccessMessage"] = null;

            return View(new StateOfficerLoginViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(StateOfficerLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var officer = await _context.StateUsers
                .FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if (officer == null)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View(model);
            }

            HttpContext.Session.SetInt32("StateOfficerId", officer.Id);
            HttpContext.Session.SetString("StateOfficerName", officer.FullName);

            return RedirectToAction("Dashboard");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("StateOfficerId");
            HttpContext.Session.Remove("StateOfficerName");
            return RedirectToAction("Login");
        }


        // =========================
        // PROFILE (VIEW)
        // =========================

        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            int? officerId = HttpContext.Session.GetInt32("StateOfficerId");

            if (officerId == null)
                return RedirectToAction("Login");

            var officer = await _context.StateUsers
                .FirstOrDefaultAsync(x => x.Id == officerId);

            if (officer == null)
                return NotFound();

            return View(officer);
        }


        // =========================
        // PROFILE (EDIT - GET)
        // =========================

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            int? officerId = HttpContext.Session.GetInt32("StateOfficerId");

            if (officerId == null)
                return RedirectToAction("Login");

            var officer = await _context.StateUsers
                .FirstOrDefaultAsync(x => x.Id == officerId);

            if (officer == null)
                return NotFound();

            return View(officer);
        }


        // =========================
        // PROFILE (EDIT - POST)
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(StateOfficer model)
        {
            int? officerId = HttpContext.Session.GetInt32("StateOfficerId");

            if (officerId == null)
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            var officer = await _context.StateUsers
                .FirstOrDefaultAsync(x => x.Id == officerId);

            if (officer == null)
                return NotFound();

            // Update allowed fields
            officer.FullName = model.FullName;
            officer.Email = model.Email;
            officer.State = model.State;
            officer.Password = model.Password; // should be hashed ideally

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Profile updated successfully!";
            HttpContext.Session.SetString("StateOfficerName", officer.FullName);

            return RedirectToAction("ViewProfile");
        }


        // =========================
        // DASHBOARD
        // =========================

        [HttpGet]
        public async Task<IActionResult> Dashboard(
            string studentSearch,
            int studentPage = 1,
            string instituteSearch = null,
            int institutePage = 1)
        {
            int? officerId = HttpContext.Session.GetInt32("StateOfficerId");
            if (officerId == null)
                return RedirectToAction("Login");

            string officerName = HttpContext.Session.GetString("StateOfficerName") ?? "State Officer";

            // ---- STUDENT APPLICATIONS ----
            var studentQuery = _context.ScholarshipApplications.AsQueryable();

            studentQuery = studentQuery.Where(a => a.FinalDeclarationAccepted);

            if (!string.IsNullOrWhiteSpace(studentSearch))
            {
                var term = studentSearch.ToLower();
                studentQuery = studentQuery.Where(a =>
                    a.UserId.ToLower().Contains(term) ||
                    a.InstituteName.ToLower().Contains(term) ||
                    a.AadhaarNumber.Contains(term));
            }

            int totalStudent = await studentQuery.CountAsync();
            int studentTotalPages = (int)Math.Ceiling(totalStudent / (double)PageSize);

            if (studentPage < 1) studentPage = 1;
            if (studentPage > studentTotalPages && studentTotalPages > 0)
                studentPage = studentTotalPages;

            var studentApplications = await studentQuery
    .OrderBy(a => a.ApplicationId)   // ASCENDING ORDER
    .Skip((studentPage - 1) * PageSize)
    .Take(PageSize)
    .ToListAsync();


            // ---- INSTITUTE REGISTRATIONS ----
            var instituteQuery = _context.Institutes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(instituteSearch))
            {
                var term = instituteSearch.ToLower();
                instituteQuery = instituteQuery.Where(i =>
                    i.InstituteName.ToLower().Contains(term) ||
                    i.InstituteCode.ToLower().Contains(term));
            }

            int totalInstitute = await instituteQuery.CountAsync();
            int instituteTotalPages = (int)Math.Ceiling(totalInstitute / (double)PageSize);

            if (institutePage < 1) institutePage = 1;
            if (institutePage > instituteTotalPages && instituteTotalPages > 0)
                institutePage = instituteTotalPages;

            var institutes = await instituteQuery
                .OrderBy(i => i.InstituteName)
                .Skip((institutePage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var vm = new StateDashboardViewModel
            {
                OfficerName = officerName,
                StudentSearch = studentSearch,
                StudentPage = studentPage,
                StudentTotalPages = studentTotalPages,
                StudentApplications = studentApplications,
                InstituteSearch = instituteSearch,
                InstitutePage = institutePage,
                InstituteTotalPages = instituteTotalPages,
                Institutes = institutes
            };

            return View(vm);
        }


        // =========================
        // STUDENT PROCESSING
        // =========================

        [HttpGet]
        public async Task<IActionResult> ViewStudent(int id)
        {
            if (HttpContext.Session.GetInt32("StateOfficerId") == null)
                return RedirectToAction("Login");

            var app = await _context.ScholarshipApplications
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (app == null)
                return NotFound();

            return View(app);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessStudent(int id, string decision, string remarks)
        {
            if (HttpContext.Session.GetInt32("StateOfficerId") == null)
                return RedirectToAction("Login");

            var app = await _context.ScholarshipApplications
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (app == null)
                return NotFound();

            app.StateStatus = decision == "Approve" ? "ForwardedToMinistry"
                            : decision == "Reject" ? "Rejected"
                            : "Pending";

            app.StateRemarks = remarks;
            app.StateActionDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Student application processed successfully!";
            return RedirectToAction("Dashboard");
        }


        // =========================
        // INSTITUTE PROCESSING
        // =========================

        [HttpGet]
        public async Task<IActionResult> ViewInstitute(int id)
        {
            if (HttpContext.Session.GetInt32("StateOfficerId") == null)
                return RedirectToAction("Login");

            var inst = await _context.Institutes
                .FirstOrDefaultAsync(i => i.InstituteId == id);

            if (inst == null)
                return NotFound();

            return View(inst);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessInstitute(int id, string decision, string remarks)
        {
            if (HttpContext.Session.GetInt32("StateOfficerId") == null)
                return RedirectToAction("Login");

            var inst = await _context.Institutes
                .FirstOrDefaultAsync(i => i.InstituteId == id);

            if (inst == null)
                return NotFound();

            inst.StateStatus = decision == "Approve" ? "ForwardedToMinistry"
                               : decision == "Reject" ? "Rejected"
                               : "Pending";

            inst.StateRemarks = remarks;
            inst.StateActionDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Institute registration processed successfully!";
            return RedirectToAction("Dashboard");
        }

    }
}
