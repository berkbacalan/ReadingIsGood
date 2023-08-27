namespace ReadingIsGood.Application.Responses;

public class BaseResponse
{
    public string? Error { get; set; }
    public bool IsSuccessful { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}