using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Domain.Events;
using UserService.Infrastructure.Sql.Entities;

namespace UserService.Infrastructure.Sql.Handlers
{
    public class UserCreatedEventHandler : INotificationHandler<CreateUserRequestEvent>
    {
        private readonly UserServiceDbContext _dbContext;

        public UserCreatedEventHandler(UserServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateUserRequestEvent notification, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = notification.AggregateRootId,
                Email = notification.Email,
                EncryptionKeyId = notification.AggregateRootId
            };

            _dbContext.Add(user);

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}