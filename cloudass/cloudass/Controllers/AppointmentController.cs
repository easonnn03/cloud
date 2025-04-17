using cloudass.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult Appointment2(string FullName, int Age, string PhoneNumber)
        {
            // Generate random 6-digit OTP
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();

            // Store it in TempData or Session (for later verification)
            TempData["GeneratedOTP"] = otp;

            // Optional: display it in the view (for testing only)
            ViewBag.OTP = otp;

            if (!string.IsNullOrEmpty(FullName) && Age > 0 && !string.IsNullOrEmpty(PhoneNumber))
            {
                TempData["Full Name"] = FullName;
                TempData["Age"] = Age;
                TempData["Phone Number"] = PhoneNumber;
            }

            TempData.Keep("Selected Service");

            return View();
        }

        public IActionResult Appointment3()
        {
            TempData.Keep("Selected Service");

            return View();
        }

        public IActionResult Summary(string SelectedTime)
        {
            if (!string.IsNullOrEmpty(SelectedTime))
            {
                TempData["Selected Time"] = SelectedTime;
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
