using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Queries;

namespace UserService.Infrastructure.Sql.Handlers
{
    public class SearchUsersQueryHandler : ISearchUsersQueryHandler
    {
        private readonly UserServiceDbContext _dbContext;

        public SearchUsersQueryHandler(UserServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserViewModel>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users
                .Where(x => EF.Functions.Like(x.Email, $"%{request.Email}%"))
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return users.Select(x => new UserViewModel
            {
                Id = x.Id,
                Email = x.Email,
                IsEmailVerified = x.IsEmailVerified,
                UpdatedAt = x.UpdatedAt
            });
        }
    }
}