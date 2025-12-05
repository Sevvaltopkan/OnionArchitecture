using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.AppUserCommands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.AppUserQueries;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.AppUserResults;

namespace OnionVb02.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAppUserQuery());
            
            if (result.IsFailure)
                return BadRequest(result);
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _mediator.Send(new GetAppUserByIdQuery(id));
            
            if (result.IsFailure)
                return NotFound(result);
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateAppUserCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result.IsFailure)
                return BadRequest(result);
            
            return CreatedAtAction(nameof(GetUserById), new { id = result.Data.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateAppUserCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result.IsFailure)
                return BadRequest(result);
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _mediator.Send(new RemoveAppUserCommand(id));
            
            if (result.IsFailure)
                return BadRequest(result);
            
            return Ok(result);
        }
    }
}
