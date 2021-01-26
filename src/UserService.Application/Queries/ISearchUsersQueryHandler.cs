using System.Collections.Generic;
using MediatR;
using UserService.Domain.Entities;

namespace UserService.Application.Queries
{
    public interface ISearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, IEnumerable<UserViewModel>>
    {
        
    }
}