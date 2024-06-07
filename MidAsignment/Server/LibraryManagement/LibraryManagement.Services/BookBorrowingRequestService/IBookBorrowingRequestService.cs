using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IBookBorrowingRequestService<BookBorrowingRequestDTO, BookBorrowingRequestCreateDTO>
    {
        Task<IEnumerable<BookBorrowingRequest>> GetAllAsync();
        Task<IEnumerable<BookBorrowingRequest>> GetByFieldAsync(string json);
        Task<List<BookBorrowingRequestDTO>> GetRequestsForCurrentMonthAsync(int userId);
        Task<BookBorrowingRequestDTO> GetByIdAsync(int Id);
        Task<BookBorrowingRequest> CreateAsync(BookBorrowingRequestCreateDTO userCreateDTO);
        void UpdateAsync(int id, BookBorrowingRequestCreateDTO userDTO);
        void DeleteAsync(int Id);
    }
}

