//Entry Point 
//Boostrap application
//configure services and middleware 
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using cloudass.Data;
using System;
using cloudass.Repository;
using cloudass.Services;
using cloudass.Models.DbTable;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("cloudassContextConnection") ?? throw new InvalidOperationException("Connection string 'cloudassContextConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

IdentityBuilder identityBuilder = builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, cloudass.Services.AppointmentService>();
builder.Services.AddScoped<IDentalServiceRepository, DentalServiceRepository>();
builder.Services.AddScoped<IDentalService, cloudass.Services.DentalService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, cloudass.Services.PatientService>();
// Adds mvc support: Controller + Views(enables controller based routing: HomeController handling requests)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//create a app instance (actual running web app)
var app = builder.Build();

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
