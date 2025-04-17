namespace cloudass.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime StartTime { get; set; }
        public int ServiceId { get; set; }
        public string? Notes { get; set; }
        public AppointmentStatus status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled,
        Missed
    }
}
