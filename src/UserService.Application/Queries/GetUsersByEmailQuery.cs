using System.Collections.Generic;
using MediatR;
using UserService.Domain.Entities;

namespace UserService.Application.Queries
{
    public class GetUsersByEmailQuery : IRequest<IEnumerable<User>>
    {
        public string EmailPart { get; set; }
    }
}