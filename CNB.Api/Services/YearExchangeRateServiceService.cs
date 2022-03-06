using CNB.Api.Clients;
using CNB.Api.Helpers;
using CNB.Api.Models;

namespace CNB.Api.Services
{
    public class YearExchangeRateServiceService : AbstractExchangeRateService<List<DayOfYearExchangeRate>, ExchangeRateParameter<YearExchangeRateFilter>, YearExchangeRateFilter>
    {
        public YearExchangeRateServiceService(CNBExchangeRateClient cnbClient) : base(cnbClient)
        {
        }

        protected override async Task<Stream> LoadTextFile(string fileIdentifier)
        {
            return await cnbClient.GetTextFileExchangeRatesByYearAsync(fileIdentifier);
        }

        protected override List<DayOfYearExchangeRate> ParseData(Stream stream)
        {
            var columnIdentifiers = new List<string>();
            var yearExchangeRates = new List<DayOfYearExchangeRate>();

            foreach (var textLine in TextFileHelper.ReadTextFileLine(stream))
            {
                var columns = textLine.Split("|")
                    .Select((x, i) => new { value = x, index = i })
                    .ToList();

                if (columns.First().value == "Datum")
                {
                    columnIdentifiers = columns.Select(x => x.value)
                        .ToList();
                    continue;
                }

                var day = DateTime.Parse(columns.First().value);
                var dayExchangeRates = (
                        from column in columns.GetRange(1, columns.Count - 1)
                        let columnIdentifier = columnIdentifiers[column.index].Split(" ")
                        let amount = int.Parse(columnIdentifier[0])
                        let code = columnIdentifier[1]
                        let value = decimal.Parse(column.value)
                        select new ExchangeRate { Amount = amount, Code = code, Value = value })
                    .ToList();
                yearExchangeRates.Add(new DayOfYearExchangeRate
                {
                    Day = day,
                    ExchangeRates = dayExchangeRates
                });
            }

            return yearExchangeRates;
        }

        protected override void ApplyFilters(YearExchangeRateFilter filters)
        {
            if (filters.Day != null)
            {
                var day = ExchangeRateData.FirstOrDefault(x => x.Day == filters.Day);
                ExchangeRateData = new List<DayOfYearExchangeRate>();

                if (day != null)
                {
                    ExchangeRateData.Add(day);
                }
            }

            if (filters.Code != null)
            {
                var helperExchangeRates = new List<DayOfYearExchangeRate>();

                foreach (var dayOfYearExchangeRate in ExchangeRateData)
                {
                    var exchangeRates = dayOfYearExchangeRate.ExchangeRates
                        .Where(x => x.Code.ToLower() == filters.Code.ToLower())
                        .ToList();

                    if (exchangeRates.Any())
                    {
                        helperExchangeRates.Add(new DayOfYearExchangeRate
                        {
                            Day = dayOfYearExchangeRate.Day,
                            ExchangeRates = exchangeRates
                        });
                    }
                }

                ExchangeRateData = helperExchangeRates;
            }
        }
    }
}
