namespace CNB.Api.Models
{
    public class DayExchangeRateDetails
    {
        public DateTime Day { get; set; }
        public int SerialNumber { get; set; }
        public List<ExchangeRateDetail> ExchangeRateDetails { get; set; }
    }
}
