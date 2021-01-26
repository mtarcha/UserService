using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Email);
            await _repository.SaveChangesAsync(user, cancellationToken).ConfigureAwait(false);

            return user.Id;
        }
    }
}