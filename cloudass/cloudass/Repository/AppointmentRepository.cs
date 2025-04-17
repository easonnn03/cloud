using cloudass.Data;
using System;
using cloudass.Models.DbTable;
using cloudass.Models;
using Microsoft.EntityFrameworkCore;

namespace cloudass.Repository
{
    public class AppointmentRepository : IAppointmentRepository 
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentDetails?> GetAppointmentDetailsByIdAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return null;

            var patient = await _context.Patients.FindAsync(appointment.PatientId);
            var service = await _context.AppointmentServices.FindAsync(appointment.ServiceId);

            var details = new AppointmentDetails
            {
                Id = appointment.Id,
                status = appointment.status.ToString(),
                CreatedTime = appointment.CreatedAt,
                PatientName = patient?.FullName ?? "",
                PatientPhone = patient?.Phone ?? "",
                AppointmentDate = appointment.StartTime,
                DentistService = service?.ServiceName ?? "",
                Duration = service?.Duration ?? TimeSpan.Zero,
                PatientNotes = appointment.Notes ?? ""
            };

            return details;
        }

        public async Task<bool> CheckAppointmentExistsByIdAsync(int id)
        {
            return await _context.Appointments.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return false;

            if (appointment.status == AppointmentStatus.Cancelled || appointment.status == AppointmentStatus.Missed || appointment.status == AppointmentStatus.Completed)
                return false;

            appointment.status = AppointmentStatus.Cancelled;
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        //JJ Part
        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
