using DAL.DBCore;
using Models.Domain.Entites;

namespace DAL.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CoreDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
