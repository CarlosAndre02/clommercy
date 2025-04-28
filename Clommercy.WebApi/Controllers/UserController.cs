using AutoMapper;
using MediatR;

using Clommercy.Core.Users.UseCases.CreateUser;
using Clommercy.Core.Users.UseCases.GetUser;
using Clommercy.Core.Users.UseCases.UpdateUser;

using Microsoft.AspNetCore.Mvc;
using Clommercy.Core.Users.UseCases.DeleteUser;

namespace Clommercy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMapper _mapper;
        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var getUserRequest = new GetUserRequest(id);
            var user = await _mediator.Send(getUserRequest);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var user = await _mediator.Send(request);

            return CreatedAtAction(
                nameof(GetUser),
                new { id = user.Id },
                _mapper.Map<CreateUserResponse>(user)
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserResponse>> Update(int id, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest();

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteUserResponse>> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var deleteUserRequest = new DeleteUserRequest(id.Value);

            var response = await _mediator.Send(deleteUserRequest);
            return Ok(response);
        }
    }
}
