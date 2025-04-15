using System.ComponentModel.DataAnnotations;

namespace cloudass.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan duration { get; set; }
        public int ServiceId { get; set; }
        public string? Notes { get; set; }
        public AppointmentStatus status { get; set; }
    }

    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled
    }
}
