using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface ICategoryService<CategoryDTO, CategoryCreateDTO>
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(int Id);
        Task<Category> CreateAsync(CategoryCreateDTO userCreateDTO);
        void UpdateAsync(int id, CategoryCreateDTO userDTO);
        void DeleteAsync(int Id);
    }
}

