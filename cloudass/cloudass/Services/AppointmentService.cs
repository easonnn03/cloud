using System.Net.Http;
using System.Text;
using cloudass.Models;
using cloudass.Models.DbTable;
using cloudass.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace cloudass.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly HttpClient _httpClient;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _httpClient = new HttpClient();
        }

        public async Task<AppointmentDetailsViewModel?> GetAppointmentDetailsByIdAsync(int AppointmentId)
        {
            return await _appointmentRepository.GetAppointmentDetailsByIdAsync(AppointmentId);
        }

        public async Task<bool> AppointmentExistsAsync(int id)
        {
            return await _appointmentRepository.CheckAppointmentExistsByIdAsync(id);
        }

        public async Task<bool> CancelAppointmentAsync(int id)
        {
            var updated = await _appointmentRepository.DeleteAsync(id);
            if (!updated)
                return false;

            await _appointmentRepository.SaveChangesAsync();
            return true;
        }

        [HttpPost]
        public async Task<AppointmentModel?> AddAppointmentAsync(AppointmentModel appointment)
        {
             AppointmentModel? added_appointment = await _appointmentRepository.AddAppointmentAsync(appointment);

            if (added_appointment != null) { 
                return added_appointment;
            }

            return null;
        }

        [HttpPost]
        public async Task<bool> SendOtp(string patient_phone)
        {
            var requestUrl = "https://3yirhx5eby4fjcnk275hfl7qry0rkiro.lambda-url.us-east-1.on.aws/";
            string phone_number = "+6" + patient_phone;
            var payload = new
            {
                phone_number = phone_number,
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(requestUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // You can handle the response here if needed
                    Console.WriteLine(result);
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPost]
        public async Task<bool> VerifyOtp(string otp,string patient_phone)
        {
            var verifyOtpUrl = "https://6v444th74ttbzqnjcyuit2mzn40aekcj.lambda-url.us-east-1.on.aws/"; // Your verify Lambda function URL
            string phone_number = "+6" + patient_phone;
            Console.WriteLine($"Verify Otp: {otp}");
            Console.WriteLine($"Phone Number: {phone_number}");
            var payload = new
            {
                phone_number = phone_number,
                otp = otp
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(verifyOtpUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Phone verified: " + result); // Success message
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Failed verify phone: " + error); // Error message
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred: " + ex.Message);
                return false;
            }
        }

    }
}
