using cloudass.Models;
using cloudass.Models.DbTable;

namespace cloudass.Repository
{
    public interface IPatientRepository
    {
        PatientModel? getPatient(int id);
        Task<PatientModel?> AddPatientAsync(PatientModel patient);
    }
}
