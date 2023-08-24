using ReadingIsGood.Domain.Entities.Base;

namespace ReadingIsGood.Domain.Entities;

public class OrderItem : Entity
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
}