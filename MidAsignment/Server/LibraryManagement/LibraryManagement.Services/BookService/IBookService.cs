using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IBookService<BookDTO, BookCreateDTO>
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<BookDTO> GetByIdAsync(int Id);
        Task<Book> CreateAsync(BookCreateDTO entityDTO);
        void UpdateAsync(int id, BookCreateDTO userDTO);
        void DeleteAsync(int Id);
    }
}

