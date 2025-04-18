using System.Diagnostics;
using System.Text;
using cloudass.Models;
using cloudass.Models.DbTable;
using cloudass.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace cloudass.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDentalService _dentalService;
        
        public AppointmentController(IAppointmentService appointmentService, IPatientService patientService, IDentalService dentalService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _dentalService = dentalService;
        }

        [HttpGet]
        public IActionResult PersonalDetail(string service)
        {
            HttpContext.Session.SetString("SelectedService", service);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PersonalDetail(PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("Patient", JsonConvert.SerializeObject(patient));

                // Await SendOtp()
                //bool otpSent = await _appointmentService.SendOtp(patient.Phone);
                //if (otpSent)
                //{
                //    return Redirect("OTP");
                //}
                //else {
                //    ViewBag.Error = "OTP Sent Failed.";
                //    return View();
                //}
                return Redirect("TimeSlot");
            }
            ViewBag.Error = "Model State Invalid.";
            return View(patient);
        }

        [HttpGet]
        public IActionResult OTP()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OTP(string otp)
        {
            var patientJson = HttpContext.Session.GetString("Patient");
            var patient = JsonConvert.DeserializeObject<PatientModel>(patientJson);
            bool verified = await _appointmentService.VerifyOtp(otp,patient.Phone);

            if (verified)
            {
                return Redirect("TimeSlot");
            }
            else
            {
                ViewBag.Error = "OTP Verify failed. Please retry.";
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> TimeSlot()
        {
            var serviceName = HttpContext.Session.GetString("SelectedService");
            var service = _dentalService.getServiceByNameAsync(serviceName);
            if (service is null) return RedirectToAction("Index", "Home");

            // Every appointment that already exists
            var appts = await _appointmentService.GetAllAsync();
            if (appts is null || !appts.Any()) return RedirectToAction("Index", "Home");

            /* --------------------------------------------------------------------
               Build a flat list like  ["2025‑04‑20 09:00", "2025‑04‑20 09:30", …]
               covering **every** half‑hour block already occupied, no matter how
               long the existing appointment is.
               -------------------------------------------------------------------- */
            var booked = new List<string>();

            foreach (var a in appts)
            {
                // pull the service for THIS appointment so you know its length
                var s = await _dentalService.getServiceAsync(a.ServiceId);
                if (s is null) continue;

                for (int k = 0; k < s.RequiredSlot; k++)
                {
                    booked.Add(a.StartTime
                                 .AddMinutes(k * 30)
                                 .ToString("yyyy-MM-dd HH:mm"));
                }
            }

            ViewBag.BookedSlots = JsonConvert.SerializeObject(booked);

            /* -------------------------------------------------------------------- */
            var vm = new TimeSlotViewModel
            {
                all_appointments = appts,
                slot_needed = service.RequiredSlot   // how many blocks THIS user needs
            };
            /* -------------------------------------------------------------------- */

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> TimeSlot(DateTime start_time)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("SelectedTime", JsonConvert.SerializeObject(start_time));
                return Redirect("Summary");
            }
            return View(start_time);
        }

        [HttpGet]
        public IActionResult SearchAppointment()
        {
            return View();
        }

        //Retrieve Appointment Details 
        [HttpPost]
        public async Task<IActionResult> SearchAppointment(int AppointmentId)
        {
            bool appointmentExists = await _appointmentService.AppointmentExistsAsync(AppointmentId);

            if (appointmentExists == false)
            {
                ViewBag.Error = "Appointment not found.";
                return View();
            }

            return RedirectToAction("AppointmentInfo", new { id = AppointmentId });
        }

        [HttpGet]
        public async Task<IActionResult> AppointmentInfo(int id)
        {
            var appointmentDetail = await _appointmentService.GetAppointmentDetailsByIdAsync(id);
            if (appointmentDetail == null)
            {
                return View("NotFound");
            }
            return View(appointmentDetail);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            bool result = await _appointmentService.CancelAppointmentAsync(id);

            ViewBag.IsSuccess = result;
            return View("AppointmentCancellationResult");
        }

        [HttpGet]
        public IActionResult Summary()
        {
            var patientJson = HttpContext.Session.GetString("Patient");
            var patient = JsonConvert.DeserializeObject<PatientModel>(patientJson);

            var selectedService = HttpContext.Session.GetString("SelectedService");

            var timeJson = HttpContext.Session.GetString("SelectedTime");
            var selectedTime = JsonConvert.DeserializeObject<DateTime>(timeJson);

            AppointmentSummaryViewModel viewModel = new();
            viewModel.Full_Name = patient.FullName;
            viewModel.Phone = patient.Phone;
            viewModel.Email = patient.Email;
            viewModel.SelectedTime = selectedTime;
            viewModel.SelectedService = selectedService;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Summary(bool confirmed) {

            if (confirmed) {
                var patientJson = HttpContext.Session.GetString("Patient");
                var patient = JsonConvert.DeserializeObject<PatientModel>(patientJson);

                var selectedService = HttpContext.Session.GetString("SelectedService");

                var timeJson = HttpContext.Session.GetString("SelectedTime");
                var selectedTime = JsonConvert.DeserializeObject<DateTime>(timeJson);

                // Add Patient or Check Existing Patient
                PatientModel? added_patient = await _patientService.AddPatientAsync(patient);
                // Get service by name
                AppointmentServiceModel? asm = _dentalService.getServiceByNameAsync(selectedService);

                if (added_patient == null) { ViewBag.Error = "Add patient failed. Redirect to home."; Redirect("/"); }
                if (asm == null) { ViewBag.Error = "Service not found. Redirect to home."; Redirect("/"); }

                //Add appointment
                AppointmentModel new_appointment = new() { PatientId = added_patient.Id, StartTime = selectedTime, status = AppointmentStatus.Scheduled, ServiceId = asm.ServiceId };
                var add_appointment = await _appointmentService.AddAppointmentAsync(new_appointment);
                if (add_appointment == null) { ViewBag.Error = "Add Appointment Failed. Redirect to home."; Redirect("/"); }
                
                Redirect("BookScheduled");
            }

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult BookScheduled()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
