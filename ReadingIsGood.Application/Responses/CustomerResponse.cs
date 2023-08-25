namespace ReadingIsGood.Application.Responses;

public class CustomerResponse
{
    public bool IsSuccessful { get; set; }
    public string Error { get; set; }
    public int Id { get; set; }
    public string Email { get; set; } = "";
}