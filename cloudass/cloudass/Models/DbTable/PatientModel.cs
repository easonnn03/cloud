using System.ComponentModel.DataAnnotations;

namespace cloudass.Models.DbTable
{
    public class PatientModel
    {
        [Key]
        public int Id { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
