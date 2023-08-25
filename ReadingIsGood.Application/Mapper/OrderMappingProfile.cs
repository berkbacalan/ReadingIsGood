using AutoMapper;
using ReadingIsGood.Application.Commands.OrderCreate;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Mapper;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderCreateCommand>().ReverseMap();
        CreateMap<Order, OrderResponse>().ReverseMap();
    }
}