using cloudass.Models;

namespace cloudass.Services
{
    public interface IAppointmentService
    {
        //Xuen Part 
        Task<Appointment?> GetAppointmentByIdAsync(int AppointmentId);

        //JJ Part
        Appointment GetAppointment(string id);
        Task AddAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(string id);
        Task UpdateAppointmentAsync(Appointment appointment);

    }

}