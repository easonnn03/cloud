using cloudass.Models.DbTable;
using cloudass.Models;
using cloudass.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cloudass.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        //Xuen Part
        public async Task<AppointmentDetails?> GetAppointmentDetailsByIdAsync(int AppointmentId)
        {
            return await _appointmentRepository.GetAppointmentDetailsByIdAsync(AppointmentId);
        }

        public async Task<bool> AppointmentExistsAsync(int id)
        {
            return await _appointmentRepository.CheckAppointmentExistsByIdAsync(id);
        }

        public async Task<bool> CancelAppointmentAsync(int id)
        {
            var updated = await _appointmentRepository.DeleteAsync(id);
            if (!updated)
                return false;

            await _appointmentRepository.SaveChangesAsync();
            return true;
        }

        //JJ Part
        /*
        public Appointment GetAppointment(string id)
        {
            return null;
        }

        [HttpPost]
        public async Task AddAppointmentAsync(Appointment appointment)
        {

            //await _appointmentRepository.AddAppointmentAsync(appointment);
            Console.WriteLine("Succesful Add");
        }

        public async Task DeleteAppointmentAsync(string id)
        {
            Console.WriteLine("Succesful Delete");
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            Console.WriteLine("Successful Update");
        }
        */

    }
}
