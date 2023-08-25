using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Application.Commands.CustomerCreate;
using ReadingIsGood.Application.Queries;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderResponse>> CustomerCreate([FromBody] CustomerCreateCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccessful)
        {
            return Ok(result);
        }

        return Problem(result.Error);
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
}