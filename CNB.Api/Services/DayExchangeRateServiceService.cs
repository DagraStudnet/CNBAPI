using CNB.Api.Clients;
using CNB.Api.Helpers;
using CNB.Api.Models;

namespace CNB.Api.Services
{
    public class DayExchangeRateServiceService : AbstractExchangeRateService<DayExchangeRateDetails, ExchangeRateParameter<DayExchangeRateFilter>, DayExchangeRateFilter>
    {
        public DayExchangeRateServiceService(CNBExchangeRateClient cnbClient) : base(cnbClient)
        {
        }

        protected override async Task<Stream> LoadTextFile(string fileIdentifier) =>
            await cnbClient.GetTextFileExchangeRatesByDay(fileIdentifier);


        protected override DayExchangeRateDetails ParseData(Stream stream)
        {
            var exchangeRateDetails = new List<ExchangeRateDetail>();
            DateTime day = default;
            int serialNumber = default;
            foreach (var (line, index) in TextFileHelper.ReadTextFileLine(stream)
                         .Select((line, index) => (line, i: index)))
            {
                if (index == 0)
                {
                    var splitDateWithSerialNumber = line.Split(" #");
                    day = DateTime.Parse(splitDateWithSerialNumber[0]);
                    serialNumber = int.Parse(splitDateWithSerialNumber[1]);
                }

                if (index < 2)
                {
                    continue;
                }

                var splitTextLine = line.Split("|");
                var rateDay = new ExchangeRateDetail
                {
                    Country = splitTextLine[0],
                    CurrencyName = splitTextLine[1],
                    Amount = int.Parse(splitTextLine[2]),
                    Code = splitTextLine[3],
                    Value = decimal.Parse(splitTextLine[4])
                };

                exchangeRateDetails.Add(rateDay);
            }

            return new DayExchangeRateDetails
            {
                Day = day,
                SerialNumber = serialNumber,
                ExchangeRateDetails = exchangeRateDetails
            };
        }

        protected override void ApplyFilters(DayExchangeRateFilter filters)
        {
            if (filters.Code != null)
            {
                ExchangeRateData.ExchangeRateDetails = ExchangeRateData.ExchangeRateDetails
                     .Where(x => x.Code.ToLower() == filters.Code.ToLower())
                     .ToList();
            }

            if (filters.Country != null)
            {
                ExchangeRateData.ExchangeRateDetails = ExchangeRateData.ExchangeRateDetails
                    .Where(x => x.Country.ToLower() == filters.Country.ToLower())
                    .ToList();
            }

            if (filters.CurrencyName != null)
            {
                ExchangeRateData.ExchangeRateDetails = ExchangeRateData.ExchangeRateDetails
                    .Where(x => x.CurrencyName.ToLower() == filters.CurrencyName.ToLower())
                    .ToList();
            }
        }
    }
}
