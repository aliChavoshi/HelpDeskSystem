using System.Reflection;
using AspNetCoreRateLimit;
using HelpDeskSystem.Data;
using HelpDeskSystem.Entities;
using HelpDeskSystem.Interfaces;
using HelpDeskSystem.Mapping;
using HelpDeskSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    // options.UseQueryTrackingBehavior()
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<CustomUserManager>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //done
builder.Services.AddScoped<ITicketRepository, TicketRepository>(); //DI
builder.Services.AddScoped<ICommentRepository, CommentRepository>(); //DI
builder.Services.AddMemoryCache(option =>
{
    //
});
//Rate Limit
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules =
    [
        new RateLimitRule
        {
            Endpoint = "*", // اعمال محدودیت روی همه مسیرها
            Limit = 5, // تعداد درخواست مجاز
            Period = "10s" // در بازه زمانی ۱۰ ثانیه
        }
    ];
});
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>(); //DI Rate Limit

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseIpRateLimiting(); // Rate Limit
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();