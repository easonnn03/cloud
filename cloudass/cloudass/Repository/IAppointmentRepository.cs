using cloudass.Models;

namespace cloudass.Repository
{
    public interface IAppointmentRepository
    {
        Task AddAppointmentAsync(Appointment appointment);
    }
}
