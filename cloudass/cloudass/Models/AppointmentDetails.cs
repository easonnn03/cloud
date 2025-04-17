namespace cloudass.Models
{
    public class AppointmentDetails
    {   
        //View only
        public int Id { get; set; }
        public string status { get; set; }
        public DateTime CreatedTime { get; set; }


        //Editable 
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string DentistService { get; set; }
        public TimeSpan Duration { get; set; }
        public string PatientNotes { get; set; }
    }
}
