namespace ReadingIsGood.Application.Responses;

public class CustomerResponse : BaseResponse
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
}