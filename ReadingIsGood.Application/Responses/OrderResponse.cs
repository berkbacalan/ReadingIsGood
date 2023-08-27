using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Responses;

public class OrderResponse : BaseResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
}