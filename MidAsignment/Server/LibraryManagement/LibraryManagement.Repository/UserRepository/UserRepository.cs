using LibraryManagement.Models;
using LibraryManagement.Repository.BaseRepository;

namespace LibraryManagement.Repository.UserRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LibraryManagementContext dbContext) : base(dbContext)
        {
        }
    }
}

