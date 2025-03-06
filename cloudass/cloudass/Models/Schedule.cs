using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DoctorSchedule
{
    public required string ID { get; set; }
    public required int appointmentID { get; set; }
    public required DateTime timeSlot {get; set;}
}
