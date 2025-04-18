using cloudass.Models.DbTable;
using cloudass.Repository;

namespace cloudass.Services
{
    public class DentalService : IDentalService
    {
        private readonly IDentalServiceRepository _dentalServiceRepository;
        public DentalService(IDentalServiceRepository dentalServiceRepository) {
            _dentalServiceRepository = dentalServiceRepository;
        }
        public async Task<AppointmentServiceModel?> getServiceAsync(int id) { 
            var result = await _dentalServiceRepository.getServiceAsync(id);
            if (result != null)
            {
                var sid = result.ServiceId.ToString();
                Console.WriteLine("Service Found: " + sid);
                return result;
            }
            else {
                Console.WriteLine("Service not found: " + id);
                return null;
            }
        }
        public async Task<List<AppointmentServiceModel>?> getAllServiceAsync() 
        {
            var result = await _dentalServiceRepository.getAllServiceAsync();
            if (result != null)
            {
                Console.WriteLine("Get all services successfully.");
                return result;
            }
            else
            {
                Console.WriteLine("Error: Services not found");
                return new List<AppointmentServiceModel>();
            }
        }
        public async Task<bool> turnOffService(int id) { 
            bool result = await _dentalServiceRepository.turnOffService(id);
            if (!result) {
                Console.WriteLine("Service Id not found: " + id);
                return false;
            }
            Console.WriteLine("Turn off service succesfully: "+id);
            return true;
        }

        public async Task<bool> turnOnService(int id) {
            bool result = await _dentalServiceRepository.turnOnService(id);
            if (!result)
            {
                Console.WriteLine("Service Id not found: " + id);
                return false;
            }
            Console.WriteLine("Turn on service succesfully: " + id);
            return true;
        }
    }
}
