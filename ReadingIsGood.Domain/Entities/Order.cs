using ReadingIsGood.Domain.Entities.Base;

namespace ReadingIsGood.Domain.Entities;

public class Order : Entity
{
    public int CustomerId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public double TotalAmount { get; set; }
}
