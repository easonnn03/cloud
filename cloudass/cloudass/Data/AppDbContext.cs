using Microsoft.EntityFrameworkCore;
using cloudass.Models;

namespace cloudass.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 101,
                    FullName = "HappyMan",
                    Email = "Happyman20@gmail.com",
                    Phone = "012-3457890"
                }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment {
                        Id = 201,
                        PatientId = 101,
                        StartTime = DateTime.Now,
                        duration = TimeSpan.FromHours(1.5),
                        status = AppointmentStatus.Scheduled
                    }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}

