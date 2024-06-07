using LibraryManagement.Models;
using LibraryManagement.Repository.BaseRepository;

namespace LibraryManagement.Repository.BookRepository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryManagementContext dbContext) : base(dbContext)
        {
        }
    }
}

