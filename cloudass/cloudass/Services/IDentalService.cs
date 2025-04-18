using cloudass.Models.DbTable;

namespace cloudass.Services
{
    public interface IDentalService
    {
        Task<AppointmentServiceModel?> getServiceAsync(int id);
        AppointmentServiceModel? getServiceByNameAsync(string name);
        Task<List<AppointmentServiceModel>?> getAllServiceAsync();
        Task<bool> turnOffService(int id);
        Task<bool> turnOnService(int id);
    }
}
