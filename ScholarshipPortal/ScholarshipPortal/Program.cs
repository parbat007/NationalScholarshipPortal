using Microsoft.EntityFrameworkCore;
using ScholarshipPortal.Data;

var builder = WebApplication.CreateBuilder(args);

// =======================================================
// 1. ADD SERVICES
// =======================================================

// Register EF Core DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MVC services
builder.Services.AddControllersWithViews();

// ENABLE SESSION 👇
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // session expire time
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// =======================================================
// 2. BUILD APP
// =======================================================
var app = builder.Build();

// =======================================================
// 3. CONFIGURE MIDDLEWARE
// =======================================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// ⚠ IMPORTANT: SESSION MUST COME BEFORE ROUTING
app.UseSession();

app.UseRouting();

app.UseAuthorization();

// Default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
