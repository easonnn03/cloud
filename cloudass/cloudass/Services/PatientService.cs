using cloudass.Models.DbTable;
using cloudass.Repository;

namespace cloudass.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository) {
            _patientRepository = patientRepository;
        }

        public async Task<PatientModel?> AddPatientAsync(PatientModel patient)
        {
            PatientModel? result = await _patientRepository.AddPatientAsync(patient);
            if (result != null)
            {
                Console.WriteLine("Succesfully Add Patient");
                return result;
            }
            else {
                Console.WriteLine("Db Add Patient Failed");
                return null;
            }

        }

        public PatientModel? FindPatientAsync(int id) {
            PatientModel? patient = _patientRepository.FindPatientAsync(id);
            return patient;
        }

        public List<PatientModel>? GetAllPatientAsync() {
            return _patientRepository.GetAllPatientAsync();
        }
    }
}
