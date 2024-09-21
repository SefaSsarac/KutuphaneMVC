using System.ComponentModel.DataAnnotations;

namespace KutuphaneMVC.Models
{
    public class SingUpViewModel
    {
        // User's full name for registration
        public string FullName { get; set; }

        // User's email address for registration
        public string Email { get; set; }

        // User's password for registration
        public string Password { get; set; }

        // Confirmation of the user's password
        [Compare(nameof(Password))] // Ensures Password and PasswordConfirm match
        public string PasswordConfirm { get; set; }

        // User's phone number for contact
        public string PhoneNumber { get; set; }

        // User's registration date (defaults to current date)
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; } = DateTime.Now;
    }
}
