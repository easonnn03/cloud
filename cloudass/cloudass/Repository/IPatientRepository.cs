using cloudass.Models;
using cloudass.Models.DbTable;

namespace cloudass.Repository
{
    public interface IPatientRepository
    {
        Task<PatientModel?> AddPatientAsync(PatientModel patient);

        PatientModel? FindPatientAsync(int id);

        List<PatientModel>? GetAllPatientAsync();
    }
}
