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

        return BadRequest(result.Error);
    }
    
    [HttpGet("GetOrdersByCustomerId/{customerId}")]
    [ProducesResponseType(typeof(PagedResponse<OrderResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<PagedResponse<OrderResponse>>> GetOrdersByCustomerId(int customerId, int page = 1, int pageSize = 10)
    {
        var query = new GetOrdersByCustomerIdQuery(customerId);
        var orders = await _mediator.Send(query);

        var orderResponses = orders as OrderResponse[] ?? orders.ToArray();
        if (!orderResponses.Any())
        {
            return NotFound();
        }

        var totalItems = orderResponses.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var pagedOrders = orderResponses.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = new PagedResponse<OrderResponse>
        {
            Data = pagedOrders,
            PageNumber = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };

        return Ok(response);
    }
}