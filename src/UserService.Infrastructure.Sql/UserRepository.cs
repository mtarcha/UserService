using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Common;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.Sql
{
    public class UserRepository : IUserRepository
    {
        private readonly IEventStore<Guid> _eventStore;
        private readonly UserServiceDbContext _dbContext;

        public UserRepository(IEventStore<Guid> eventStore, UserServiceDbContext dbContext)
        {
            _eventStore = eventStore;
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateUserAsync(string email, CancellationToken token)
        {
            var userExists = await _dbContext.Users.AnyAsync(x => x.Email == email, token);
            if (userExists)
            {
                // todo: throw specific exception
                throw new Exception("User with such email exists");
            }

            var user = new User(email);
            await _dbContext.AddAsync(user, token);

            return user.Id;
        }

        public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken token)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, token);
        }

        public async Task<IReadOnlyCollection<User>> GetUsersByEmailPartAsync(string emailPart, CancellationToken token)
        {
            return await _dbContext.Users.Where(x => EF.Functions.Like(x.Email, $"%{emailPart}%")).ToListAsync(token);
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            foreach (var user in _dbContext.ChangeTracker.Entries<User>())
            {
                await _eventStore.AddEventsAsync(user.Entity.ChangeSet, token);
            }

            await _dbContext.SaveChangesAsync(token);
        }
    }
}