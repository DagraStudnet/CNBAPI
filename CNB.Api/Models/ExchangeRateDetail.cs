namespace CNB.Api.Models;

public class ExchangeRateDetail : ExchangeRate
{
    public string Country { get; set; }
    public string CurrencyName { get; set; }
}