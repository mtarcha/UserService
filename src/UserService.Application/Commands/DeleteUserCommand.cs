using System;
using MediatR;

namespace UserService.Application.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}