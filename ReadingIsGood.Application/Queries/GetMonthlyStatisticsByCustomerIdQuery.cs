using MediatR;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Application.Queries;

public class GetMonthlyStatisticsByCustomerIdQuery : IRequest<IEnumerable<CustomerStatisticsResponse>>
{
    public int CustomerId { get; set; }

    public GetMonthlyStatisticsByCustomerIdQuery(int customerId)
    {
        CustomerId = customerId;
    }
}