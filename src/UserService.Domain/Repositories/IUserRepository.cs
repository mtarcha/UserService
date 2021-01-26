using System;
using UserService.Domain.Common;
using UserService.Domain.Entities;

namespace UserService.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}