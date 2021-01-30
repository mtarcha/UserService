using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Events;

namespace UserService.Infrastructure.Sql.Handlers
{
    public class UserDeletedEventHandler : INotificationHandler<DeleteUserRequestEvent>
    {
        private readonly UserServiceDbContext _dbContext;

        public UserDeletedEventHandler(UserServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteUserRequestEvent request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == request.AggregateRootId, cancellationToken)
                .ConfigureAwait(false);

            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}