using DAL.DBCore;
using Models.Domain.Entites;

namespace DAL.Repositories
{
    public interface IRegisterUserRepository : IGenericRepository<RegisterUser>
    {
    }
    public class RegisterUserRepository : GenericRepository<RegisterUser>, IRegisterUserRepository
    {
        public RegisterUserRepository(CoreDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
