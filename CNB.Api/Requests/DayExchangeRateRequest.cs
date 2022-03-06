namespace CNB.Api.Requests;

public class DayExchangeRateRequest
{
    public DateTime Day { get; set; }
    public string? Code { get; set; }
    public string? CurrencyName { get; set; }
    public string? Country { get; set; }
}