using cloudass.Models;

namespace cloudass.Services
{
    public interface IAppointmentService
    {
        Appointment GetAppointment(string id);
        Task AddAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(string id);
        Task UpdateAppointmentAsync(Appointment appointment);

    }

}
