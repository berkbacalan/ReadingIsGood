using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
}