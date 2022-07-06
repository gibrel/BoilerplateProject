using Boilerplate.Data.Context;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Interfaces;

namespace Boilerplate.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }
    }
}
