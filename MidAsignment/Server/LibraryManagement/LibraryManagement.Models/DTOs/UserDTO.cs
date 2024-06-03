namespace LibraryManagement.Models
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsSuperUser { get; set; }
    }
}
