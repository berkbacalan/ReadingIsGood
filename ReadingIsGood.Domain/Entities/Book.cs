using ReadingIsGood.Domain.Entities.Base;

namespace ReadingIsGood.Domain.Entities;

public class Book : Entity
{
    public string Title { get; set; } = "";
    public int StockQuantity { get; set; }
    public decimal BookPrice { get; set; }
}