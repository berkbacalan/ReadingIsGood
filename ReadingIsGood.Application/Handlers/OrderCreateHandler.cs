using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using ReadingIsGood.Application.Commands.OrderCreate;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;

namespace ReadingIsGood.Application.Handlers;

public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderCreateHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderResponse> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderEntity = _mapper.Map<Order>(request);
            if (orderEntity == null)
            {
                throw new ValidationException("Order Entity could not mapped");
            }

            var order = await _orderRepository.AddAsync(orderEntity);

            var orderResponse = _mapper.Map<OrderResponse>(order);
            return orderResponse;
        }
        catch (Exception e)
        {
            throw new ApplicationException($"Error happened during Order creation, Error: {e}");
        }
    }
}