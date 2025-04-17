using cloudass.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace cloudass.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Appointment(string service)
        {
            if (!string.IsNullOrEmpty(service))
            {
                TempData["Selected Service"] = service;
            }

            return View();
        }

        public IActionResult Appointment2(string FullName, string Email, string PhoneNumber)
        {
            // Generate random 6-digit OTP
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();

            // Store it in TempData or Session (for later verification)
            TempData["GeneratedOTP"] = otp;

            // Optional: display it in the view (for testing only)
            ViewBag.OTP = otp;

            if (!string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(PhoneNumber))
            {
                TempData["Full Name"] = FullName;
                TempData["Email"] = Email;
                TempData["Phone Number"] = PhoneNumber;
            }

            TempData.Keep("Selected Service");


            return View();
        }

        public IActionResult Appointment3()
        {
            Dictionary<string, int> serviceDurations = new Dictionary<string, int>
            {
                { "Teeth Whitening", 2 },    // 1 hour
                { "Bracing", 3 },           // 1.5 hours
                { "Tooth Fillings", 2 },          // 1 hour
                { "Tooth Extraction", 1 }   // 30 minutes
            };

            var service = TempData["Selected Service"]?.ToString();

            int slotsNeeded = serviceDurations.ContainsKey(service) ? serviceDurations[service] : 1;

            ViewBag.SlotsNeeded = slotsNeeded;

            var bookedSlots = new List<string>
            {
                "2025-04-02 09:00",
                "2025-04-02 09:30",
                "2025-04-02 10:00",
                "2025-04-03 14:00",
                "2025-04-03 14:30",
                "2025-04-03 15:00"
            };

            ViewBag.BookedSlots = JsonSerializer.Serialize(bookedSlots);

            TempData.Keep("Selected Service");
            TempData.Keep("Full Name");
            TempData.Keep("Email");
            TempData.Keep("Phone Number");

            return View();
        }

        public IActionResult Summary(string SelectedTime)
        {
            if (!string.IsNullOrEmpty(SelectedTime))
            {
                TempData["Selected Time"] = SelectedTime;
            }


            TempData.Keep("Selected Service");
            TempData.Keep("Full Name");
            TempData.Keep("Email");
            TempData.Keep("Phone Number");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> View_Appointment(int id)
        {
            var appointment = new AppointmentDetails
            {
                Id = 1,
                status = "Scheduled",
                CreatedTime = DateTime.Now.AddDays(-2),

                PatientName = "John Doe",
                PatientPhone = "012-3456789",
                AppointmentDate = new DateTime(2025, 4, 20, 9, 30, 0),
                DentistService = "Teeth Cleaning",
                Duration = TimeSpan.FromMinutes(30),
                PatientNotes = "Routine cleaning before vacation."
            };

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }
        
        public async Task<IActionResult> View_Appointment(string AppointmentId)
        {
            if (!int.TryParse(AppointmentId, out int id))
            {
                TempData["LookupError"] = "Invalid appointment ID format.";
                return RedirectToAction("LookupPage"); // or wherever your form is
            }

            var appointment = new AppointmentDetails
            {
                Id = 1,
                status = "Scheduled",
                CreatedTime = DateTime.Now.AddDays(-2),

                PatientName = "John Doe",
                PatientPhone = "012-3456789",
                AppointmentDate = new DateTime(2025, 4, 20, 9, 30, 0),
                DentistService = "Teeth Cleaning",
                Duration = TimeSpan.FromMinutes(30),
                PatientNotes = "Routine cleaning before vacation."
            };

            if (appointment != null)
            {
                return RedirectToAction("My_Appointment", new { id = appointment.Id });
            }
            else
            {
                TempData["LookupError"] = "Appointment not found. Please check your ID.";
                return RedirectToAction("LookupPage"); // replace with actual view
            }
        }
    }
}
