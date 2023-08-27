using MediatR;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Application.Queries;

public class GetOrdersByDateQuery : IRequest<IEnumerable<OrderResponse>>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public GetOrdersByDateQuery(DateTime? startDate, DateTime? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}