using AutoMapper;
using MediatR;
using ReadingIsGood.Application.Queries;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Repositories;

namespace ReadingIsGood.Application.Handlers;

public class GetBooksHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookResponse>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBooksHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var bookList = await _bookRepository.GetAllAsync();
        var response = bookList.Select(book => _mapper.Map<BookResponse>(book));

        return response;
    }
}