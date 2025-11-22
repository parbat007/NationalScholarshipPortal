using Microsoft.EntityFrameworkCore;
using ScholarshipPortal.Data;

// --- STEP 1: CREATE HOST BUILDER ---
var builder = WebApplication.CreateBuilder(args);

// =======================================================
// 1. ADD SERVICES (MUST be before builder.Build())
// =======================================================

// Register EF Core/SQL Server DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MVC services
builder.Services.AddControllersWithViews();

// =======================================================
// 2. BUILD THE APPLICATION
// =======================================================
var app = builder.Build();

// =======================================================
// 3. CONFIGURE MIDDLEWARE (app.Use... and app.Map...)
// =======================================================

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();