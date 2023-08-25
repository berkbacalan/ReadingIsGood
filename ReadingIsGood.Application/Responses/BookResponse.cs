namespace ReadingIsGood.Application.Responses;

public class BookResponse
{
    public bool IsSuccessful { get; set; }
    public string Error { get; set; }
    public int Id { get; set; }
    public string Title { get; set; } = "Unknown Title";
    public int StockQuantity { get; set; }
    public double BookPrice { get; set; }
}