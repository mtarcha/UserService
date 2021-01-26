using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Events;

namespace UserService.Infrastructure.Sql.Handlers
{
    public class EmailChangedEventHandler : IRequestHandler<ChangeEmailRequestEmail>
    {
        private readonly UserServiceDbContext _dbContext;

        public EmailChangedEventHandler(UserServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ChangeEmailRequestEmail request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .SingleOrDefaultAsync(x => x.Id == request.AggregateRootId, cancellationToken)
                .ConfigureAwait(false);

            user.Email = request.NewEmail;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}