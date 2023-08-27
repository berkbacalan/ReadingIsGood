using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Responses;

public class CustomerStatisticsResponse : BaseResponse
{
    public int Year { get; set; }
    public string Month { get; set; } = "";
    public int TotalOrderCount { get; set; }
    public int TotalBookCount { get; set; }
    public double TotalPurchasedAmount { get; set; }
}