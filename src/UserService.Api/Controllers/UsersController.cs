using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get([FromQuery(Name = "emailPart")] string emailPart)
        {
            var query = new GetUsersByEmailQuery
            {
                EmailPart = emailPart
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}