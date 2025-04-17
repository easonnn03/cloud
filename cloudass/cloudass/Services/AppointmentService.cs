using cloudass.Models;
using cloudass.Repository;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<Appointment?> GetAppointmentByIdAsync(int AppointmentId)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(AppointmentId);
        }


        //JJ Part
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

    }
}
