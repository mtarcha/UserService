using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserService.Domain.Common;
using UserService.Domain.Entities;

namespace UserService.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<Guid> CreateUserAsync(string email, CancellationToken token);

        Task<IReadOnlyCollection<User>> FindUsersAsync(string email, CancellationToken token);
    }
}