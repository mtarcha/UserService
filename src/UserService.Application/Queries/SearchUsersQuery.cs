using System.Collections.Generic;
using MediatR;
using UserService.Domain.Entities;

namespace UserService.Application.Queries
{
    public class SearchUsersQuery : IRequest<IEnumerable<User>>
    {
        public string Email { get; set; }
    }
}