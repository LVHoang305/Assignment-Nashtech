using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IBookService<BookDTO, BookCreateDTO>
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<BookDTO> GetByIdAsync(int Id);
        void CreateAsync(BookCreateDTO entityDTO);
        void UpdateAsync(int id, BookDTO userDTO);
        void DeleteAsync(int Id);
    }
}

