using cloudass.Data;
using cloudass.Models.DbTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace cloudass.Repository
{
    public class DentalServiceRepository : IDentalServiceRepository
    {
        private readonly AppDbContext _context;
        public DentalServiceRepository(AppDbContext context) { 
            _context = context;
        }

        public async Task<AppointmentServiceModel?> getServiceAsync(int id) { 
            var service = await _context.AppointmentServices.FindAsync(id);
            if (service == null) return null;
            return service;
        }

        public AppointmentServiceModel? getServiceByNameAsync(string name)
        {
            var service = _context.AppointmentServices.FirstOrDefault(s => s.ServiceName == name);
            if (service == null) return null;
            return service;
        }

        public async Task<List<AppointmentServiceModel>?> getAllServiceAsync() { 
            var servicesList = await _context.AppointmentServices.ToListAsync();
            if (servicesList.IsNullOrEmpty()) return null;
            return servicesList;
        }
        public async Task<bool> turnOffService(int id)
        {
            var service = await _context.AppointmentServices.FindAsync(id);
            if (service == null) return false;

            service.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> turnOnService(int id)
        {
            var service = await _context.AppointmentServices.FindAsync(id);
            if (service == null) return false;

            service.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
