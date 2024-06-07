using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IBookBorrowingRequestDetailsService<BookBorrowingRequestDetailsDTO, BookBorrowingRequestDetailsCreateDTO>
    {
        Task<IEnumerable<BookBorrowingRequestDetails>> GetAllAsync();
        Task<BookBorrowingRequestDetailsDTO> GetByIdAsync(int Id);
        Task<BookBorrowingRequestDetails> CreateAsync(BookBorrowingRequestDetailsCreateDTO userCreateDTO);
        void UpdateAsync(int id, BookBorrowingRequestDetailsCreateDTO userDTO);
        void DeleteAsync(int Id);
    }

}

