using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.OrderDetailCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.OrderDetailQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderDetailResults;

namespace OnionVb02.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var result = await _mediator.Send(new GetAllOrderDetailsQuery());
            
            if (result.IsFailure)
                return BadRequest(result);
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var result = await _mediator.Send(new GetOrderDetailByIdQuery(id));
            
            if (result.IsFailure)
                return NotFound(result);
            
            return Ok(result);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
        {
            var result = await _mediator.Send(new GetOrderDetailsByOrderIdQuery(orderId));
            
            if (result.IsFailure)
                return BadRequest(result);
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result.IsFailure)
                return BadRequest(result);
            
            return CreatedAtAction(nameof(GetOrderDetailById), new { id = result.Data.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result.IsFailure)
                return BadRequest(result);
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var result = await _mediator.Send(new RemoveOrderDetailCommand(id));
            
            if (result.IsFailure)
                return BadRequest(result);
            
            return Ok(result);
        }
    }
}

