//foundation of the web app
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using cloudass.Data;
using cloudass.Areas.Identity.Data;

//configure logging, configuration, dependency injection and etc.
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("cloudassContextConnection") ?? throw new InvalidOperationException("Connection string 'cloudassContextConnection' not found.");

builder.Services.AddDbContext<cloudassContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<cloudassContext>();

// Adds mvc support: Controller + Views(enables controller based routing: HomeController handling requests)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//create a app instance (actual running web app)
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//redirect all http to https
app.UseHttpsRedirection();

//use static files like CSS, Javascript, images from wwwroot
app.UseStaticFiles();

//MVC routing
app.UseRouting();
app.UseAuthentication();
//Restrict pages to logged-in users
app.UseAuthorization();

//defines the default route for MVC (where to send requests)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
//start the server
app.Run();
