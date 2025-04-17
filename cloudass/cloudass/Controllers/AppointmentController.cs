using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cloudass.Models;
using cloudass.Services;
using System.Diagnostics;
using cloudass.Models.DbTable;

namespace cloudass.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        //Xuen Part
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





        //Chong Heng Part
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
