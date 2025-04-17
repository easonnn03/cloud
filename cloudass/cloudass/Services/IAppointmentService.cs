using cloudass.Models.DbTable;
using cloudass.Models;

namespace cloudass.Services
{
    public interface IAppointmentService
    {
        //Xuen Part (use task when async)
        Task<AppointmentDetails?> GetAppointmentDetailsByIdAsync(int AppointmentId);
        Task<bool> AppointmentExistsAsync(int id);
        Task<bool> CancelAppointmentAsync(int id);

        //JJ Part
        /*
        Appointment GetAppointment(string id);
        Task AddAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(string id);
        Task UpdateAppointmentAsync(Appointment appointment);
        */
    }

}