using cloudass.Models.DbTable;

namespace cloudass.Services
{
    public interface IPatientService
    {
        Task<PatientModel?> AddPatientAsync(PatientModel patient);
    }
}
