using LibraryManagement.Models;
using LibraryManagement.Repository.BaseRepository;

namespace LibraryManagement.Repository.BookBorrowingRequestRepository
{
    public class BookBorrowingRequestRepository : BaseRepository<BookBorrowingRequest>, IBookBorrowingRequestRepository
    {
        public BookBorrowingRequestRepository(LibraryManagementContext dbContext) : base(dbContext)
        {
        }
    }
}

