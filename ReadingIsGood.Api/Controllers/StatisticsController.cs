using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Application.Commands.OrderCreate;
using ReadingIsGood.Application.Handlers;
using ReadingIsGood.Application.Queries;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetMonthlyStatisticsByCustomerId/{customerId}")]
    [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderResponse>> GetMonthlyStatisticsByCustomerId(int customerId)
    {
        var query = new GetMonthlyStatisticsByCustomerIdQuery(customerId);
        var statistics = await _mediator.Send(query);

        return Ok(statistics);
    }
}