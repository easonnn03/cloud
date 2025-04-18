using cloudass.Models.DbTable;

namespace cloudass.Models
{
    public class TimeSlotViewModel
    {
        public List<AppointmentModel> all_appointments { get; set; }

        public int slot_needed { get; set; }
    }
}
