using MediatR;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Application.Commands.Book;

public class BookCreateCommand : IRequest<BookResponse>
{
    public string Title { get; set; } = "";
    public int StockQuantity { get; set; }
    public double BookPrice { get; set; }
}