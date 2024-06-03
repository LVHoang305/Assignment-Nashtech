using System;
namespace LibraryManagement.Models.CreateDTOs
{
	public class UserCreateDTO
	{
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsSuperUser { get; set; }
    }
}

