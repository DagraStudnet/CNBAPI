using CNB.Api.Configuration;
using Refit;

namespace CNB.Api.Clients
{
    public class CNBExchangeRateClient
    {
        private readonly ICNBExchangeRateClient client;

        public CNBExchangeRateClient(CNBApiConfiguration configuration)
        {
            client = RestService.For<ICNBExchangeRateClient>(configuration.Url);
        }

        public async Task<Stream> GetTextFileExchangeRatesByYearAsync(string year) =>
            await client.GetTextFileExchangeRatesByYear(year);


        public async Task<Stream> GetTextFileExchangeRatesByDay(string day) =>
            await client.GetTextFileExchangeRatesByDay(day);
        
    }
}
