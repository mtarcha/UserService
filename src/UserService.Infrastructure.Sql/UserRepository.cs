using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;
using UserService.Infrastructure.EventSourcing;

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
            var userExists = await _dbContext.Users.AnyAsync(x => x.Email == email, token).ConfigureAwait(false);
            if (userExists)
            {
                // todo: throw specific exception
                throw new Exception("User with such email exists");
            }

            var user = new User(email);
            _dbContext.Add(user);

            return user.Id;
        }

        public async Task<IReadOnlyCollection<User>> FindUsersAsync(string email, CancellationToken token)
        {
            return await _dbContext.Users.Where(x => EF.Functions.Like(x.Email, $"%{email}%")).ToListAsync(token).ConfigureAwait(false);
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, token).ConfigureAwait(false);
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            var updates = _dbContext.ChangeTracker.Entries<User>().ToList();
            foreach (var user in updates)
            {
                await _eventStore.AddEventsAsync(user.Entity.ChangeSet, token).ConfigureAwait(false);
                if (user.Entity.IsDeleted)
                {
                    _dbContext.Users.Remove(user.Entity);
                }
            }

            await _dbContext.SaveChangesAsync(token);
        }
    }
}