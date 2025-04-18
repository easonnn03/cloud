namespace cloudass.Models
{
    public class AppointmentSummaryViewModel
    {
        public string Full_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string SelectedService { get; set; }

        public DateTime SelectedTime { get; set; }
    }
}
