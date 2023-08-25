using AutoMapper;
using MediatR;
using ReadingIsGood.Application.Queries;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Repositories;

namespace ReadingIsGood.Application.Handlers;

public class GetOrdersByCustomerIdHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IEnumerable<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersByCustomerIdHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var orderList = await _orderRepository.GetOrdersByCustomerId(request.CustomerId);
        var response = orderList.Select(order => _mapper.Map<OrderResponse>(order));

        return response;
    }
}