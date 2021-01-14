using System;
using MediatR;

namespace UserService.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Email { get; set; }
    }
}