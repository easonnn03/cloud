using cloudass.Data;
using cloudass.Models;
using cloudass.Models.DbTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace cloudass.Repository
{
    public class AppointmentRepository:IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentDetailsViewModel?> GetAppointmentDetailsByIdAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return null;

            var patient = await _context.Patients.FindAsync(appointment.PatientId);
            var service = await _context.AppointmentServices.FindAsync(appointment.ServiceId);

            var details = new AppointmentDetailsViewModel
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

        public async Task<List<AppointmentModel>?> GetAllAsync() {
            var appointmentList = await _context.Appointments.ToListAsync();
            if (appointmentList.IsNullOrEmpty()) return null;
            return appointmentList;
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

        public async Task<AppointmentModel?> AddAppointmentAsync(AppointmentModel appointment)
        {
            try
            {
                var entityEntry = await _context.Appointments.AddAsync(appointment);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    Console.WriteLine("SaveChangesAsync returned 0, insert failed!");
                    return null;
                }

                // Verify appointment exists
                var insertedAppointment = await _context.Appointments.FindAsync(entityEntry.Entity.Id);
                if (insertedAppointment == null)
                {
                    Console.WriteLine($"Appointment ID {entityEntry.Entity.Id} not found after insert.");
                    return null;
                }

                return insertedAppointment;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Insert failed with error: {ex.Message}");
                return null;
            }
        }


        public async Task DeleteAppointmentAsync(AppointmentModel appointment)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
