namespace cloudass.Models
{
    public class EmailRequiredModel
    {
        public string PatientName { get; set; }   
        public string Email { get; set; }    
        public int AppointmentId { get; set; }                
        public string ServiceName { get; set; }   
        public DateTime DateTimeUtc { get; set; }
    }
}
