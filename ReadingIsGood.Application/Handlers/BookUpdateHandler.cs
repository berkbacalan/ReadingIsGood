using System.Data;
using AutoMapper;
using FluentValidation;
using MediatR;
using ReadingIsGood.Application.Commands.Book;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;

namespace ReadingIsGood.Application.Handlers;

public class BookUpdateHandler : IRequestHandler<BookUpdateCommand, BookResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookUpdateHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<BookResponse> Handle(BookUpdateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);
            book.StockQuantity = request.StockQuantity;
            await _bookRepository.UpdateAsync(book);

            var response = _mapper.Map<BookResponse>(book);
            response.IsSuccessful = true;
            return response;
            
        }
        catch (DataException e)
        {
            return new BookResponse
                { IsSuccessful = false, Error = $"Book could not found with given Id: {request.BookId}" };
        }
        catch (Exception e)
        {
            return new BookResponse { IsSuccessful = false, Error = $"Unknown error occured. Error: {e}"};
        }
    }
}