using ReadingIsGood.Domain.Entities.Base;

namespace ReadingIsGood.Domain.Entities;

public class Order : Entity
{
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
}
