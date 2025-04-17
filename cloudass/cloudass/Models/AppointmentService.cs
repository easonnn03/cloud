namespace cloudass.Models
{
    public class AppointmentService
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsActive { get; set; }
    }
}
