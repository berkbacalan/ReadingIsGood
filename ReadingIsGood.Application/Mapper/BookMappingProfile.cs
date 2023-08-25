using AutoMapper;
using ReadingIsGood.Application.Commands.Book;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Mapper;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookCreateCommand>().ReverseMap();
        CreateMap<Book, BookResponse>().ReverseMap();
        CreateMap<IEnumerable<Book>, IEnumerable<BookResponse>>();
    }
}