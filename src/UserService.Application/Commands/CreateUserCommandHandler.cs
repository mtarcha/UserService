using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
            var id = await _repository.CreateUserAsync(request.Email, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return id;
        }
    }
}