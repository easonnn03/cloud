using cloudass.Models.DbTable;

namespace cloudass.Repository
{
    public interface IDentalServiceRepository
    {
        Task<AppointmentServiceModel?> getServiceAsync(int id);
        Task<List<AppointmentServiceModel>?> getAllServiceAsync();
        Task<bool> turnOffService(int id);
        Task<bool> turnOnService(int id);

    }
}
