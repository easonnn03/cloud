using cloudass.Data;
using cloudass.Models;

namespace cloudass.Repository
{
    public interface IAppointmentRepository
    {
        //Xuen Part 
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        //JJ Part 
        Task AddAppointmentAsync(Appointment appointment);
    }
}
