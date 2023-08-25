using AutoMapper;
using ReadingIsGood.Application.Commands.CustomerCreate;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Mapper;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CustomerCreateCommand>().ReverseMap();
        CreateMap<Customer, CustomerResponse>().ReverseMap();
    }
}