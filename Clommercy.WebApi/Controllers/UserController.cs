using System.Threading.Tasks;

using AutoMapper;

using Clommercy.Core.Users.Domain;
using Clommercy.Core.Users.UseCases.CreateUser;
using Clommercy.Core.Users.UseCases.GetUser;

using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetUser(int id) {
            var getUserRequest = new GetUserRequest(id);
            var user = await _mediator.Send(getUserRequest);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {   
            CreateUserResponse user = await _mediator.Send(request);
            
            return CreatedAtAction(
                nameof(GetUser),
                new { id = user.Id },
                _mapper.Map<CreateUserResponse>(user)
            );
        }
    }
}
