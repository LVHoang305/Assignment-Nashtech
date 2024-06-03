namespace LibraryManagement.Services
{
    public interface IBookBorrowingRequestDetailsService<BookBorrowingRequestDetailsDTO, BookBorrowingRequestDetailsCreateDTO>
    {
        Task<IEnumerable<BookBorrowingRequestDetailsDTO>> GetAllAsync();
        Task<BookBorrowingRequestDetailsDTO> GetByIdAsync(int Id);
        void CreateAsync(BookBorrowingRequestDetailsCreateDTO userCreateDTO);
        void UpdateAsync(int id, BookBorrowingRequestDetailsDTO userDTO);
        void DeleteAsync(int Id);
    }

}

