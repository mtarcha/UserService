using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.Commands;

namespace UserService.Api.Services
{
    public class StorageSeeder : IStorageSeeder
    {
        private readonly IMediator _mediator;

        public StorageSeeder(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SeedAsync(CancellationToken token)
        {
            await _mediator.Send(new CreateUserCommand
            {
                Email = "test@test.com"
            }, token);

            await _mediator.Send(new CreateUserCommand
            {
                Email = "test+123@test.com"
            }, token);

            await _mediator.Send(new CreateUserCommand
            {
                Email = "random@random.com"
            }, token);
        }
    }
}