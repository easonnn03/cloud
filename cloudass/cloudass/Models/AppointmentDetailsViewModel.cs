namespace cloudass.Models
{
    public class AppointmentDetailsViewModel
    {   
        public int Id { get; set; }
        public required string status { get; set; }
        public DateTime CreatedTime { get; set; }
        public required string PatientName { get; set; }
        public required string PatientPhone { get; set; }
        public DateTime AppointmentDate { get; set; }
        public required string DentistService { get; set; }
        public TimeSpan Duration { get; set; }
        public required string PatientNotes { get; set; }
    }
}
