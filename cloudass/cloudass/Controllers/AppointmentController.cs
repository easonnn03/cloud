using cloudass.Models;
using cloudass.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Appointment appointment)
    {
        await _appointmentService.AddAppointmentAsync(appointment);
        return Ok(new { message = "Appointment created successfully", appointment });
    }   
}
