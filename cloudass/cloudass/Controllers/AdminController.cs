using cloudass.Services;
using cloudass.Models;
using cloudass.Models.DbTable;
using Microsoft.AspNetCore.Mvc;

namespace cloudass.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDentalService _dentalService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;

        public AdminController(IDentalService dentalService, IAppointmentService appointmentService,IPatientService patientService)
        {
            _dentalService = dentalService;
            _appointmentService = appointmentService;
            _patientService = patientService;
        }
        public async Task<IActionResult> ServicePanel()
        {
            var all_services = await _dentalService.getAllServiceAsync();
            return View(all_services);
        }

        public async Task<IActionResult> ManageServices(int id)
        {
            var services = await _dentalService.turnOnService(id);

            return View("Service_Panel");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleService(int id)
        {
            // Call your service logic (you can add toggle logic here too)
            var service = await _dentalService.getServiceAsync(id);
            if (service != null)
            {
                if (service.IsActive)
                    await _dentalService.turnOffService(id);
                else
                    await _dentalService.turnOnService(id);
            }

            // ✅ Stay on the same page by re-fetching and returning ViewAllService data
            var services = await _dentalService.getAllServiceAsync();
            return View("Service_Panel", services); // ✅ explicitly render this view with data
        }

        public async Task<IActionResult> ViewAllService()
        {
            List<AppointmentServiceModel>? services = await _dentalService.getAllServiceAsync();
            return View(services);
        }

        public async Task<IActionResult> ViewAppointment() {
            List<AppointmentModel>? appointments = await _appointmentService.GetAllAsync();
            List<AppointmentDetailsViewModel> advms = new();
            foreach (AppointmentModel appointment in appointments) {
                PatientModel? patient = _patientService.FindPatientAsync(appointment.PatientId);
                AppointmentServiceModel? service = await _dentalService.getServiceAsync(appointment.ServiceId);
                AppointmentDetailsViewModel advm = new() { AppointmentDate = appointment.StartTime, DentistService = service.ServiceName, PatientName = patient.FullName, Duration = service.Duration, PatientPhone = patient.Phone, PatientNotes = appointment.Notes, status = appointment.status.ToString(),Id=appointment.Id};
                advms.Add(advm);
            } 
            return View(advms);
        }

        [HttpPost]
        public async Task<IActionResult> CancelAppointment(int appointmentId) { 
            var result = await _appointmentService.CancelAppointmentAsync(appointmentId);
            if (!result)
            {
                ViewBag.Error = "Appointment not found.";
            }
            return Redirect("ViewAppointment");
        }

        public IActionResult Menu()
        {
            return View();
        }
    }
}