using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ScholarshipPortal.Data;
using ScholarshipPortal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
namespace ScholarshipPortal.Controllers
{
    // Ensure you have added ApplicationDbContext as a service in Program.cs
    public class HomeController : Controller
    {
        // Static data for Announcements (does not require DB access)
        private static readonly List<AnnouncementViewModel> SampleAnnouncements = new List<AnnouncementViewModel>
        {
            new AnnouncementViewModel { Title = "Scholarship Applications Open for AY 2025-2026", Description = "Applications for various scholarship schemes are now open. Students are advised to apply before the deadline.", Date = new DateTime(2025, 3, 15), Type = "New", Color = "green" },
            new AnnouncementViewModel { Title = "Extended Deadline for Post-Matric Scholarship", Description = "The deadline for Post-Matric Scholarship has been extended till March 31, 2025. Complete your applications at the earliest.", Date = new DateTime(2025, 3, 10), Type = "Urgent", Color = "red" },
            new AnnouncementViewModel { Title = "Document Verification Schedule Released", Description = "The document verification schedule for all districts has been published. Check your district schedule in your dashboard.", Date = new DateTime(2025, 3, 5), Type = "Info", Color = "orange" },
            new AnnouncementViewModel { Title = "New Merit-Based Scholarship Introduced", Description = "A new merit-based scholarship scheme has been introduced for students scoring above 85% in their previous examination.", Date = new DateTime(2025, 3, 1), Type = "New", Color = "green" },
            new AnnouncementViewModel { Title = "System Maintenance Notice", Description = "The portal will undergo scheduled maintenance on March 20, 2025 from 12:00 AM to 6:00 AM. Please plan your activities accordingly.", Date = new DateTime(2025, 2, 28), Type = "Info", Color = "orange" }
        };

        // --- EF CORE DEPENDENCY INJECTION ---
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Index or / (Homepage)
        public IActionResult Index() => View();

        // GET: /Home/Logout
        public IActionResult Logout()
        {
            // Clear the authentication key to log out the user
            TempData.Remove("AuthenticatedUserEmail");
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction(nameof(Index));
        }

        // --- AUTHENTICATION ACTIONS ---

        // GET: /Home/Register (Displays the registration form, passing state/district data)
        [HttpGet]
        public IActionResult Register()
        {
            var stateDistrictData = new StateDistrictViewModel();
            ViewData["StateDistrictData"] = stateDistrictData;
            return View(new RegisterViewModel());
        }

        // POST: /Home/Register (Handles registration submission and validation)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Re-populate state/district data in case validation fails
            ViewData["StateDistrictData"] = new StateDistrictViewModel();

            if (ModelState.IsValid)
            {
                // Check if user already exists (by Email or Mobile) using EF Core
                bool emailExists = await _context.RegisteredUsers.AnyAsync(u => u.Email == model.Email);
                bool mobileExists = await _context.RegisteredUsers.AnyAsync(u => u.MobileNumber == model.MobileNumber);

                if (emailExists || mobileExists)
                {
                    ModelState.AddModelError(string.Empty, "A user with this Email or Mobile Number already exists.");
                    return View(model);
                }

                // Add new user to the DbContext and save changes asynchronously
                _context.RegisteredUsers.Add(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                return RedirectToAction(nameof(RegistrationSuccess));
            }
            return View(model);
        }

        // GET: /Home/RegistrationSuccess (Displays success message and login link)
        [HttpGet]
        public IActionResult RegistrationSuccess()
        {
            if (TempData["SuccessMessage"] == null)
            {
                // Redirect if user somehow navigates here without TempData
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: /Home/Login (Displays the login form)
        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        // POST: /Home/Login (Handles login authentication)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find user by username (email or mobile) and password
                var user = await _context.RegisteredUsers
                    .FirstOrDefaultAsync(u =>
                        (u.Email == model.Username || u.MobileNumber == model.Username) &&
                        u.Password == model.Password); // NOTE: Passwords would be hashed in a real application.

                if (user != null)
                {
                    // Successful login: Store the authenticated email for subsequent dashboard requests
                    TempData["AuthenticatedUserEmail"] = user.Email;
                    TempData["SuccessMessage"] = $"Welcome back, {user.Name}!";
                    TempData.Keep("AuthenticatedUserEmail"); // Keep authentication key alive for redirection

                    return RedirectToAction(nameof(Dashboard));
                }

                // Failed login
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }
            return View(model);
        }

        // --- DASHBOARD ACTIONS ---

        // GET: /Home/Dashboard (The main user application area)
        [HttpGet]
        public async Task<IActionResult> Dashboard(string activeSection)
        {
            string userEmail = TempData.Peek("AuthenticatedUserEmail")?.ToString();

            // Authentication Check
            if (string.IsNullOrEmpty(userEmail))
            {
                TempData["ErrorMessage"] = "You must be logged in to access the dashboard.";
                return RedirectToAction(nameof(Login));
            }

            // Fetch Profile
            var userProfile = await _context.RegisteredUsers
                                   .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (userProfile == null)
            {
                TempData["ErrorMessage"] = "User profile not found.";
                TempData.Remove("AuthenticatedUserEmail");
                return RedirectToAction(nameof(Login));
            }

            // Get ALL applications submitted by student
            var userApplications = await _context.ScholarshipApplications
                                                 .Where(a => a.UserId == userEmail)
                                                 .OrderByDescending(a => a.SubmissionDate)
                                                 .ToListAsync();

            // -----------------------------
            // BUILD APPLICATION STATUS VIEW
            // -----------------------------
            var applicationStatuses = userApplications.Select(a => new ApplicationStatusViewModel
            {
                ApplicationId = a.ApplicationId,
                SchemeName = a.SchemeName,
                ApplicationDate = a.SubmissionDate,

                // Read REAL statuses
                CurrentStatus = a.MinistryStatus ?? a.StateStatus ?? "Submitted",

                StatusColor =
                    a.MinistryStatus == "Approved" ? "green" :
                    a.MinistryStatus == "Rejected" ? "red" :
                    a.StateStatus == "ForwardedToMinistry" ? "blue" :
                    a.StateStatus == "Rejected" ? "red" :
                    "yellow",  // Pending

                InstituteVerificationDate =
                    a.StateActionDate?.ToString("dd MMM yyyy") ?? "N/A"

            }).ToList();


            // -----------------------------
            // SUMMARY COUNTERS
            // -----------------------------
            var summary = new DashboardSummaryViewModel
            {
                TotalApplied = applicationStatuses.Count,
                Approved = applicationStatuses.Count(s => s.CurrentStatus == "Approved"),
                Rejected = applicationStatuses.Count(s => s.CurrentStatus == "Rejected"),
                Pending = applicationStatuses.Count(s => s.CurrentStatus == "Submitted" || s.CurrentStatus == "Pending")
            };

            // -----------------------------
            // PROGRAM CARDS
            // -----------------------------
            var schemes = new List<ProgramCardViewModel>
    {
        new ProgramCardViewModel { Title = "Post Matric Scholarship", Description = "For students pursuing higher education.", Amount = "Varies by Scheme" },
        new ProgramCardViewModel { Title = "Scholarship Meant For Girls(PRAGATI)", Description = "Promotes education for girls.", Amount = "Up to 30,000" }
    };

            // FINAL VIEWMODEL
            var viewModel = new DashboardViewModel
            {
                UserProfile = userProfile,
                Summary = summary,
                ApplicationStatuses = applicationStatuses,
                AvailableSchemes = schemes
            };

            ViewData["ActiveSection"] = activeSection;
            TempData.Keep("AuthenticatedUserEmail");

            return View(viewModel);
        }

        // POST: /Home/UpdateProfile (Handles profile edit submission and updates DB)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(RegisterViewModel updatedModel)
        {
            // 1. Get the unique identifier (Email)
            string userEmail = updatedModel.Email;

            if (string.IsNullOrEmpty(userEmail))
            {
                TempData["ErrorMessage"] = "Session expired or invalid user identifier.";
                return RedirectToAction(nameof(Login));
            }

            // --- CRITICAL FIX: MANUAL, EXPLICIT ASSIGNMENT ---

            // 2. Fetch the existing, tracked user entry (DO NOT use AsNoTracking)
            var existingEntry = await _context.RegisteredUsers
                                    .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (existingEntry == null)
            {
                TempData["ErrorMessage"] = "User profile not found in database.";
                return RedirectToAction(nameof(Login));
            }

            existingEntry.Name = updatedModel.Name;
            existingEntry.MobileNumber = updatedModel.MobileNumber;
            existingEntry.StateOfDomicile = updatedModel.StateOfDomicile;
            existingEntry.District = updatedModel.District;
            existingEntry.InstituteCode = updatedModel.InstituteCode;
            existingEntry.BankName = updatedModel.BankName;
            await _context.SaveChangesAsync();

            // 5. Success feedback and redirection
            TempData["SuccessMessage"] = "Profile updated successfully! Changes saved to the database.";

            // CRITICAL: Re-establish the authentication key for the next request
            TempData["AuthenticatedUserEmail"] = existingEntry.Email;
            TempData.Keep("AuthenticatedUserEmail");

            // Redirect back to the profile page on the dashboard
            return RedirectToAction(nameof(Dashboard), new { activeSection = "profile" });
        }


        // --- INFORMATIONAL ACTIONS ---

        // GET: /Home/Announcements (Displays the announcements timeline)
        [HttpGet]
        public IActionResult Announcements() => View(SampleAnnouncements);

        // GET: /Home/ContactUs (Displays the contact form)
        [HttpGet]
        public IActionResult ContactUs() => View(new ContactViewModel());

        // POST: /Home/ContactUs (Handles contact form submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save submission to SQL Server
                model.SubmissionDate = DateTime.Now;
                _context.ContactSubmissions.Add(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Your message has been sent successfully! We will get back to you soon.";
                return RedirectToAction(nameof(ContactUs));
            }
            return View(model);
        }

        // GET: /Home/ForgotPassword (Displays the forgot password page)
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //Scholarship Registration
      
        [HttpGet]
        public async Task<IActionResult> Apply(int step = 1, string schemeName = null)
        {
            // USER AUTH
            string userEmail = TempData.Peek("AuthenticatedUserEmail")?.ToString();
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction(nameof(Login));

            ScholarshipApplicationViewModel model = null;

            // RESTORE SAVED MODEL FROM TEMPDATA
            if (TempData.ContainsKey("ScholarshipApplication"))
            {
                string json = TempData["ScholarshipApplication"].ToString();
                model = JsonSerializer.Deserialize<ScholarshipApplicationViewModel>(json);
                TempData["ScholarshipApplication"] = json; // keep alive
            }
            else
            {
                // NEW APPLICATION (STEP 1)
                var userProfile = await _context.RegisteredUsers.AsNoTracking()
                                   .FirstOrDefaultAsync(u => u.Email == userEmail);

                model = new ScholarshipApplicationViewModel
                {
                    UserId = userEmail,
                    SchemeName = schemeName ?? "General Scholarship Scheme",
                    AadhaarNumber = userProfile?.AadhaarNumber ?? ""
                };
            }

            // STEP BOUNDS CHECK
            if (step < 1 || step > 3)
                return RedirectToAction(nameof(Dashboard));

            ViewData["CurrentStep"] = step;
            TempData.Keep("AuthenticatedUserEmail");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(
            ScholarshipApplicationViewModel model,
            int step,
            string action)
        {
            // USER AUTH
            string userEmail = TempData.Peek("AuthenticatedUserEmail")?.ToString();
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction(nameof(Login));

            ScholarshipApplicationViewModel stored = null;

            if (TempData.ContainsKey("ScholarshipApplication"))
            {
                stored = JsonSerializer.Deserialize<ScholarshipApplicationViewModel>(
                            TempData["ScholarshipApplication"].ToString());
            }

            if (stored != null)
            {
                // Merge only the fields from the current step
                foreach (var prop in GetCurrentStepProperties(step))
                {
                    var p = typeof(ScholarshipApplicationViewModel).GetProperty(prop);
                    if (p != null)
                    {
                        var newValue = p.GetValue(model);
                        p.SetValue(stored, newValue);
                    }
                }

                model = stored; // merged model
            }

            model.UserId = userEmail;

            var allowedProps = GetCurrentStepProperties(step);

            foreach (var key in ModelState.Keys.ToList())
            {
                if (!allowedProps.Contains(key))
                    ModelState.Remove(key);
            }

            if (!ModelState.IsValid)
            {
                TempData["ScholarshipApplication"] = JsonSerializer.Serialize(model);
                TempData.Keep("AuthenticatedUserEmail");
                return RedirectToAction(nameof(Apply), new { step });
            }


            if (action == "Back")
            {
                TempData["ScholarshipApplication"] = JsonSerializer.Serialize(model);
                TempData.Keep("AuthenticatedUserEmail");
                return RedirectToAction(nameof(Apply), new { step = step - 1 });
            }

            if (action == "Next")
            {
                TempData["ScholarshipApplication"] = JsonSerializer.Serialize(model);
                TempData.Keep("AuthenticatedUserEmail");
                return RedirectToAction(nameof(Apply), new { step = step + 1 });
            }

            if (action == "Submit" && step == 3)
            {
                try
                {
                    // safety defaults
                    model.ClassStartDate ??= new DateTime(2000, 1, 1);

                    // save to DB
                    _context.ScholarshipApplications.Add(model);
                    await _context.SaveChangesAsync();

                    TempData.Remove("ScholarshipApplication");
                    TempData["SuccessMessage"] = $"Application ID {model.ApplicationId} submitted!";
                    return RedirectToAction(nameof(Dashboard));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error submitting: " + ex.Message;
                    TempData["ScholarshipApplication"] = JsonSerializer.Serialize(model);
                    return RedirectToAction(nameof(Apply), new { step });
                }
            }

            return RedirectToAction(nameof(Dashboard));
        }

        private List<string> GetCurrentStepProperties(int step)
        {
            var stepProperties = new Dictionary<int, List<string>>
    {
        { 1, new List<string> {
            "AadhaarNumber","Religion","CommunityCategory","FatherName",
            "FamilyAnnualIncome","InstituteName","PresentClassCourse",
            "PresentClassCourseYear","ModeOfStudy","ClassStartDate","UniversityBoardName"
        }},
        { 2, new List<string> {
            "ContactState","ContactDistrict","BlockTaluk","HouseNumber","StreetNumber",
            "Pincode","AdmissionFee","TuitionFee","OtherFee","IsDisabled",
            "MaritalStatus","ParentsProfession"
        }},
        { 3, new List<string> { "FinalDeclarationAccepted" }}
    };

            return stepProperties.ContainsKey(step)
                ? stepProperties[step]
                : new List<string>();
        }

        // ... (Remaining existing actions) ...
    }
}