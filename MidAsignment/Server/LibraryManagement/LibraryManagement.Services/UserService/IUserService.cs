using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IUserService<UserDTO, UserCreateDTO>
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int Id);
        Task<User> GetByEmail(string email);
        Task<User> CreateAsync(UserCreateDTO userCreateDTO);
        void UpdateAsync(int id, UserDTO userDTO);
        void DeleteAsync(int Id);
        bool VerifyPassword(string password, string storedHash);

    }
}

