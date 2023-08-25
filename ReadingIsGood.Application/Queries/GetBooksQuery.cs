using MediatR;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Application.Queries;

public class GetBooksQuery : IRequest<IEnumerable<BookResponse>>
{
    public GetBooksQuery()
    {
    }
}