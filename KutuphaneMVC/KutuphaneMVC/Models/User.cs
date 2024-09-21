public class User
{
    // Unique identifier for the user
    public int Id { get; set; }

    // User's full name
    public string FullName { get; set; }

    // User's email address
    public string Email { get; set; }

    // User's password for authentication
    public string Password { get; set; }

    // User's phone number for contact
    public string PhoneNumber { get; set; }

    // Date the user joined the system
    public DateTime JoinDate { get; set; }
}
