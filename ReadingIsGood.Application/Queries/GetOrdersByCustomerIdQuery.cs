using MediatR;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Application.Queries;

public class GetOrdersByCustomerIdQuery : IRequest<IEnumerable<OrderResponse>>
{
    public int CustomerId { get; set; }

    public GetOrdersByCustomerIdQuery(int customerId)
    {
        CustomerId = customerId;
    }
}