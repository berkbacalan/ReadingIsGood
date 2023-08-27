using AutoMapper;
using MediatR;
using ReadingIsGood.Application.Queries;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Repositories;

namespace ReadingIsGood.Application.Handlers;

public class GetOrdersByDateHandler : IRequestHandler<GetOrdersByDateQuery, IEnumerable<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersByDateHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByDateQuery request, CancellationToken cancellationToken)
    {
        var orderList = await _orderRepository.GetOrdersByDate(request.StartDate, request.EndDate);
        var response = orderList.Select(order => _mapper.Map<OrderResponse>(order));
        return response;
    }
}