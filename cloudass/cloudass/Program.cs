var builder = WebApplication.CreateBuilder(args);

// Adds mvc support (enables controller based routing)
builder.Services.AddControllersWithViews();

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

//use static files like CSS, Javascript, images) in wwwroot
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
