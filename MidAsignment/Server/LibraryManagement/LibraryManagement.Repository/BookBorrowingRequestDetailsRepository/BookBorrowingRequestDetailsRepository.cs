using LibraryManagement.Models;
using LibraryManagement.Repository.BaseRepository;

namespace LibraryManagement.Repository.BookBorrowingRequestDetailsRepository
{
    public class BookBorrowingRequestDetailsRepository : BaseRepository<BookBorrowingRequestDetails>, IBookBorrowingRequestDetailRepository
    {
        public BookBorrowingRequestDetailsRepository(LibraryManagementContext dbContext) : base(dbContext)
        {
        }
    }
}

