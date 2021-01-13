using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserService.Domain.Entities;

namespace UserService.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(string email, CancellationToken token);

        Task<User> GetUserByIdAsync(Guid userId, CancellationToken token);

        Task<IReadOnlyCollection<User>> GetUsersByEmailPartAsync(string emailPart, CancellationToken token);

        Task SaveChangesAsync(CancellationToken token);
    }
}