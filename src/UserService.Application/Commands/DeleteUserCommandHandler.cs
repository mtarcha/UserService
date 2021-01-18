using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Domain.Repositories;

namespace UserService.Application.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _repository;

        public DeleteUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
            user.Delete();
            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}