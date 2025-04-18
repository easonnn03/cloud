using cloudass.Models.DbTable;

namespace cloudass.Services
{
    public interface IPatientService
    {
        Task<PatientModel?> AddPatientAsync(PatientModel patient);
        PatientModel? FindPatientAsync(int id);

        List<PatientModel>? GetAllPatientAsync();
    }
}
