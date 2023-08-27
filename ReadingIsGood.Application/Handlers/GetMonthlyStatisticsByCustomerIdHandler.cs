using System.Collections;
using System.Globalization;
using AutoMapper;
using MediatR;
using ReadingIsGood.Application.Queries;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Repositories;

namespace ReadingIsGood.Application.Handlers;

public class GetMonthlyStatisticsByCustomerIdHandler : IRequestHandler<GetMonthlyStatisticsByCustomerIdQuery, IEnumerable<CustomerStatisticsResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetMonthlyStatisticsByCustomerIdHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerStatisticsResponse>> Handle(GetMonthlyStatisticsByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersByCustomerId(request.CustomerId);

        var statisticsByMonth = orders
            .GroupBy(o => new { o.CreatedOn.Year, o.CreatedOn.Month })
            .Select(group => new CustomerStatisticsResponse
            {
                Year = group.Key.Year,
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month),
                TotalOrderCount = group.Count(),
                TotalBookCount = group.Sum(o => o.OrderItems.Sum(oi => oi.Quantity)),
                TotalPurchasedAmount = group.Sum(o => o.TotalAmount)
            })
            .ToList();

        return statisticsByMonth;
    }
}