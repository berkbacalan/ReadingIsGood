using MediatR;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Application.Commands.Book;

public class BookUpdateCommand : IRequest<BookResponse>
{
    public int BookId { get; set; }
    public int StockQuantity { get; set; }
}