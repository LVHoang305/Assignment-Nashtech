using System;
namespace LibraryManagement.Services
{
    public interface IBookBorrowingRequestService<BookBorrowingRequestDTO, BookBorrowingRequestCreateDTO>
    {
        Task<IEnumerable<BookBorrowingRequestDTO>> GetAllAsync();
        Task<BookBorrowingRequestDTO> GetByIdAsync(int Id);
        void CreateAsync(BookBorrowingRequestCreateDTO userCreateDTO);
        void UpdateAsync(int id, BookBorrowingRequestDTO userDTO);
        void DeleteAsync(int Id);
    }
}

