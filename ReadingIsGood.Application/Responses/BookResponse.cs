namespace ReadingIsGood.Application.Responses;

public class BookResponse : BaseResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = "Unknown Title";
    public int StockQuantity { get; set; }
    public decimal BookPrice { get; set; }
}