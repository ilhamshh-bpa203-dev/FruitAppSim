using FruitSimulation.Data;
using FruitSimulation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;

    opt.Password.RequiredLength = 8;

    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    opt.Lockout.MaxFailedAccessAttempts = 3;
}).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();


var app = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
