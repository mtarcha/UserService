using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Queries;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "emailPart")] string emailPart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetUsersByEmailQuery
            {
                EmailPart = emailPart
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}