using cloudass.Models.DbTable;
using cloudass.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace cloudass.Repository
{
    public class PatientRepository:IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public PatientModel? getPatient(int id) { 
            return _context.Patients.Find(id);
        }

        public async Task<PatientModel?> AddPatientAsync(PatientModel patient)
        {
            try
            {
                var existingPatient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.Phone == patient.Phone);

                if (existingPatient != null)
                {
                    // Patient with same phone already exists
                    return existingPatient;
                }

                var entityEntry = await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();
                var added_patient = entityEntry.Entity;
                return added_patient;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
