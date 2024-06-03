using System;
namespace LibraryManagement.Services
{
    public interface ICategoryService<CategoryDTO, CategoryCreateDTO>
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(int Id);
        void CreateAsync(CategoryCreateDTO userCreateDTO);
        void UpdateAsync(int id, CategoryDTO userDTO);
        void DeleteAsync(int Id);
    }
}

