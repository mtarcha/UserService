using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands;
using UserService.Application.Queries;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "email")] string email, CancellationToken token)
        {
            var query = new SearchUsersQuery
            {
                Email = email
            };

            var result = await _mediator.Send(query, token).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, CancellationToken token)
        {
            var command = new CreateUserCommand
            {
                Email = email
            };

            var result = await _mediator.Send(command, token).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken token)
        {
            var command = new DeleteUserCommand
            {
                UserId = userId
            };

            var result = await _mediator.Send(command, token).ConfigureAwait(false);

            return Ok(result);
        }
    }
}