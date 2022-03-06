using Refit;

namespace CNB.Api.Clients
{
    public interface ICNBExchangeRateClient
    {
        [Get("/rok.txt?rok={year}")]
        Task<Stream> GetTextFileExchangeRatesByYear(string year);

        [Get("/denni_kurz.txt?date={day}")]
        Task<Stream> GetTextFileExchangeRatesByDay(string day);
    }
}
