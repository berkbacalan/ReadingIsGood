using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Application.Commands.Book;
using ReadingIsGood.Application.Queries;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllBooks")]
    [ProducesResponseType(typeof(IEnumerable<BookResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<BookResponse>>> GetAllBooks()
    {
        var query = new GetBooksQuery();
        var books = await _mediator.Send(query);

        return Ok(books);
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(BookResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BookResponse>> BookCreate([FromBody] BookCreateCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccessful)
        {
            return Ok(result);
        }

        return Problem(result.Error);
    }

    [HttpPost("Update")]
    [ProducesResponseType(typeof(BookResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<BookResponse>> BookUpdate([FromBody] BookUpdateCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccessful)
        {
            return Ok(result);
        }

        return Problem(result.Error);
    }
}