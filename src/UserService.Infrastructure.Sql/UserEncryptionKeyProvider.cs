using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.EventSourcing;

namespace UserService.Infrastructure.Sql
{
    public class UserEncryptionKeyProvider : IEncryptionKeysProvider<Guid>
    {
        private readonly UserServiceDbContext _dbContext;

        public UserEncryptionKeyProvider(UserServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetOrCreateEncryptionKeyAsync(Guid aggregateId, CancellationToken token)
        {
            var userKey = await _dbContext.UserEncryptionKeys.FirstOrDefaultAsync(x => x.Id == aggregateId, token);
            if (userKey == null)
            {
                userKey = new UserEncryptionKeys
                {
                    Id = aggregateId,
                    EncryptionKey = Guid.NewGuid().ToString()
                };

                _dbContext.UserEncryptionKeys.Add(userKey);
                await _dbContext.SaveChangesAsync(token);
            }

            return userKey.EncryptionKey;
        }
    }
}