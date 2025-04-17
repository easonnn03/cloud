using System.ComponentModel.DataAnnotations;

namespace cloudass.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class Appointment
    {
        [Required]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Phone number must be 10 or 11 digits.")]
        public string PhoneNumber { get; set; }
    }
    public class Appointment2
    {
        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "OTP must be a 6-digit number.")]
        public string OTP { get; set; }
    }
}
