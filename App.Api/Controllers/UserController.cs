using System.Threading.Tasks;
using App.Application.Users;
using App.Application.Users.Commands;
using App.Application.Users.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var userDto = _mapper.Map<UserDto>(request);
            var user = await _mediator.Send(new RegisterUserCommand(userDto));
            return Ok();
        }

        [Route("users")]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromRoute] bool active = true)
        {
            var users = await _mediator.Send(new GetUsersQuery(active));

            return Ok(users);
        }

        [Route("{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUser([FromRoute] int userId)
        {
            var user = await _mediator.Send(new GetUserQuery(userId));

            return Ok(user);
        }

        [Route("")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            await _mediator.Send(new UpdateUserCommand(request.User));
            return Ok();
        }


        [Route("{userId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            await _mediator.Send(new DeleteUserCommand(userId));

            return Ok();
        }

        [Route("{userId}/{projectId}")]
        [HttpPut]
        public async Task<IActionResult> AssignProjectToUser([FromRoute] int userId, [FromRoute] int projectId)
        {
            await _mediator.Send(new AssignUserToProjectCommand(userId, projectId));

            return Ok();
        }
    }
}