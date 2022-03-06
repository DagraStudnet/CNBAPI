namespace CNB.Api.Requests;

public class YearExchangeRateRequest
{
    public int Year { get; set; }
    public DateTime? Day { get; set; }
    public string? Code { get; set; }
}