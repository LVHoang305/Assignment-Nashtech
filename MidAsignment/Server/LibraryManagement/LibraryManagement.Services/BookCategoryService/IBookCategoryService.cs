using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IBookCategoryService<BookCategoryDTO, BookCategoryCreateDTO>
    {
        Task<IEnumerable<BookCategory>> GetAllAsync();
        Task<BookCategoryDTO> GetByIdAsync(int Id);
        Task<BookCategory> CreateAsync(BookCategoryCreateDTO userCreateDTO);
        void UpdateAsync(int id, BookCategoryCreateDTO userDTO);
        void DeleteAsync(int Id);
        void DeleteByFieldAsync(string fieldName, string fieldValue);
    }
}

