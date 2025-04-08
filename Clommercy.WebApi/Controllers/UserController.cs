using Clommercy.Core.Users.UseCases.CreateUser;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clommercy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {   
            var user = await _mediator.Send(request);
            
            return Ok(user);
            /*** This should be used when the getUser controller get implemented ***/
            // return CreatedAtAction(
            //     nameof(GetUser),
            //     new { id = user.Id },
            //     UserToDTO(user)
            // );
        }
    }
}
