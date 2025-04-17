using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cloudass.Models;
using cloudass.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace cloudass.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        //Retrieve Appointment Details 
        [HttpGet]
        public async Task<IActionResult> InputId(int AppointmentId)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(AppointmentId);

            if (appointment == null)
            {
                return NotFound();
            }

            return View("AppointmentDetails", appointment);
        }

        [HttpPost]
        public IActionResult View()
        {
            return View();
        }

    }
}
