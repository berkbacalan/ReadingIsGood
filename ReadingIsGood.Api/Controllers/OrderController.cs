using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Application.Commands.OrderCreate;
using ReadingIsGood.Application.Queries;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetOrdersByCustomerId/{customerId}")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByCustomerId(int customerId)
    {
        var query = new GetOrdersByCustomerIdQuery(customerId);
        var orders = await _mediator.Send(query);

        if (!orders.Any())
        {
            return NotFound();
        }

        return Ok(orders);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderResponse>> OrderCreate([FromBody] OrderCreateCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ValidationException ve)
        {
            return ValidationProblem();
        }
        catch (Exception e)
        {
            return Problem();
        }
    }
}