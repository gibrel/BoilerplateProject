using AutoMapper;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Interfaces;

namespace Boilerplate.Service.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(
            IUserRepository userRepository,
            IMapper mapper
        ) : base(userRepository, mapper)
        {
        }
    }
}
