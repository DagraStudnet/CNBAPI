using CNB.Api.Clients;

namespace CNB.Api.Services
{
    public abstract class AbstractExchangeRateService<TResult, TParameter, TFilters>
        where TParameter : ExchangeRateParameter<TFilters>
    {
        protected readonly CNBExchangeRateClient cnbClient;

        protected TResult ExchangeRateData = default!;
        protected AbstractExchangeRateService(CNBExchangeRateClient cnbClient)
        {
            this.cnbClient = cnbClient;
        }

        protected abstract Task<Stream> LoadTextFile(string fileIdentifier);
        protected abstract TResult ParseData(Stream stream);
        protected abstract void ApplyFilters(TFilters filters);

        public async Task<TResult> GetData(TParameter parameters)
        {
            var stream = await LoadTextFile(parameters.FileIdentifier);
            ExchangeRateData = ParseData(stream);
            ApplyFilters(parameters.Filter);

            return ExchangeRateData;
        }
    }
}
