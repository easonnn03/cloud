using cloudass.Models;
using cloudass.Models.DbTable;

namespace cloudass.Repository
{
    public interface IAppointmentRepository
    {
        Task<AppointmentDetailsViewModel?> GetAppointmentDetailsByIdAsync(int id);
        Task<bool> CheckAppointmentExistsByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
        Task<AppointmentModel?> AddAppointmentAsync(AppointmentModel appointment);
    }
}
