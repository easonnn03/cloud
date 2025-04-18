using System.ComponentModel.DataAnnotations;

namespace cloudass.Models.DbTable
{
    public class AppointmentServiceModel
    {
        [Key]
        public int ServiceId { get; set; }

        public string? ServiceName { get; set; }
        public TimeSpan Duration { get; set; }
        public int RequiredSlot { get; set; }
        public bool IsActive { get; set; }
    }
}
