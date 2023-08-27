using AutoMapper;
using FluentValidation;
using MediatR;
using ReadingIsGood.Application.Commands.Book;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;

namespace ReadingIsGood.Application.Handlers;

public class BookCreateHandler : IRequestHandler<BookCreateCommand, BookResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookCreateHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<BookResponse> Handle(BookCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bookEntity = _mapper.Map<Book>(request);
            if (bookEntity == null)
            {
                return new BookResponse { IsSuccessful = false, Error = "Entity could not mapped." };
            }

            var book = await _bookRepository.AddAsync(bookEntity);

            var bookResponse = _mapper.Map<BookResponse>(book);
            bookResponse.IsSuccessful = true;
            return bookResponse;
        }
        catch (Exception e)
        {
            return new BookResponse { IsSuccessful = false, Error = $"Unknown error occured." };
        }
    }
}