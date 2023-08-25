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

    [HttpGet("GetOrderDetailsById/{orderId}")]
    [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<OrderResponse>> GetOrderDetailsById(int orderId)
    {
        var query = new GetOrderByIdQuery(orderId);
        var order = await _mediator.Send(query);

        return Ok(order);
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderResponse>> OrderCreate([FromBody] OrderCreateCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccessful)
        {
            return Ok(result);
        }
        return BadRequest(result.Error);
    }
}