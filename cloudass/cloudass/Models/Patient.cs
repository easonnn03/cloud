namespace cloudass.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
} 