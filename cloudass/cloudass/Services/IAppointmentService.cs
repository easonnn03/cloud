using cloudass.Models;
using cloudass.Models.DbTable;

namespace cloudass.Services
{
    public interface IAppointmentService
    {
        Task<AppointmentDetailsViewModel?> GetAppointmentDetailsByIdAsync(int AppointmentId);
        Task<bool> AppointmentExistsAsync(int id);
        Task<bool> CancelAppointmentAsync(int id);
        Task<AppointmentModel?> AddAppointmentAsync(AppointmentModel appointment);
        Task<bool> SendOtp(string patient_phone);
        Task<bool> VerifyOtp(string otp, string patient_phone);
    }

}
