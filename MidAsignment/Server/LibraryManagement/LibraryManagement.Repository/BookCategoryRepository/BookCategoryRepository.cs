using LibraryManagement.Models;
using LibraryManagement.Repository.BaseRepository;

namespace LibraryManagement.Repository.BookCategoryRepository
{
    public class BookCategoryRepository : BaseRepository<BookCategory>, IBookCategoryRepository
    {
        public BookCategoryRepository(LibraryManagementContext dbContext) : base(dbContext)
        {
        }
    }
}

