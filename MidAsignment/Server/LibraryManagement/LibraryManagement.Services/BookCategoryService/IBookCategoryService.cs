using System;
namespace LibraryManagement.Services
{
    public interface IBookCategoryService<BookCategoryDTO, BookCategoryCreateDTO>
    {
        Task<IEnumerable<BookCategoryDTO>> GetAllAsync();
        Task<BookCategoryDTO> GetByIdAsync(int Id);
        void CreateAsync(BookCategoryCreateDTO userCreateDTO);
        void UpdateAsync(int id, BookCategoryDTO userDTO);
        void DeleteAsync(int Id);
    }
}

