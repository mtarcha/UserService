using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Events;

namespace UserService.Infrastructure.Sql.Handlers
{
    public class UserEmailVerifiedEventHandler : INotificationHandler<UserEmailVerifiedEvent>
    {
        private readonly UserServiceDbContext _dbContext;

        public UserEmailVerifiedEventHandler(UserServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UserEmailVerifiedEvent request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == request.AggregateRootId, cancellationToken)
                .ConfigureAwait(false);

            user.IsEmailVerified = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}