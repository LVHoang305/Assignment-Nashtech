namespace LibraryManagement.Services
{
    public interface IUserService<UserDTO, UserCreateDTO>
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int Id);
        void CreateAsync(UserCreateDTO userCreateDTO);
        void UpdateAsync(int id, UserDTO userDTO);
        void DeleteAsync(int Id);
    }
}

