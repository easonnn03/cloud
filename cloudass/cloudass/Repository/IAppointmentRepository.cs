using cloudass.Data;
using cloudass.Models.DbTable;
using cloudass.Models;

namespace cloudass.Repository
{
    public interface IAppointmentRepository
    {
        //Xuen Part 
        Task<AppointmentDetails?> GetAppointmentDetailsByIdAsync(int id);
        Task<bool> CheckAppointmentExistsByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();

        //JJ Part 
        Task AddAppointmentAsync(Appointment appointment);
    }
}
