namespace CNB.Api.Models
{
    public class DayOfYearExchangeRate
    {
        public DateTime Day { get; set; }
        public List<ExchangeRate> ExchangeRates { get; set; }
    }
}
