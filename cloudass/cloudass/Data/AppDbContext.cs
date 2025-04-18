using cloudass.Models.DbTable;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

/*
DbContext handles connecting to a database (with connection string)
Mapping C# classes (models) to db tables 
LINQ instead of raw SQL
*/

namespace cloudass.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppointmentModel> Appointments { get; set; }
    public DbSet<PatientModel> Patients { get; set; }
    public DbSet<AppointmentServiceModel> AppointmentServices { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
