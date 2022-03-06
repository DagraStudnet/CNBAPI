using CNB.Api.Requests;
using CNB.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNB.Api.Controllers
{
    [ApiController]
    [Route("exchange-rate")]
    public class CnbExchangeRateController : ControllerBase
    {
        private readonly YearExchangeRateServiceService yearExchangeRateServiceService;
        private readonly DayExchangeRateServiceService dayExchangeRateServiceService;


        public CnbExchangeRateController(YearExchangeRateServiceService yearExchangeRateServiceService, DayExchangeRateServiceService dayExchangeRateServiceService)
        {
            this.yearExchangeRateServiceService = yearExchangeRateServiceService;
            this.dayExchangeRateServiceService = dayExchangeRateServiceService;
        }

        [HttpGet("year")]
        public async Task<IActionResult> GetExchangeRateByYear([FromQuery] YearExchangeRateRequest yearExchangeRateRequest)
        {
            var exchangeRateParameter = new ExchangeRateParameter<YearExchangeRateFilter>()
            {
                FileIdentifier = yearExchangeRateRequest.Year.ToString(),
                Filter = new YearExchangeRateFilter()
                {
                    Code = yearExchangeRateRequest.Code,
                    Day = yearExchangeRateRequest.Day
                }
            };
            var result = await yearExchangeRateServiceService.GetData(exchangeRateParameter);
            return Ok(result);
        }

        [HttpGet("day")]
        public async Task<IActionResult> GetExchangeRateByDay([FromQuery] DayExchangeRateRequest dayExchangeRateRequest)
        {
            var exchangeRateParameter = new ExchangeRateParameter<DayExchangeRateFilter>()
            {
                FileIdentifier = dayExchangeRateRequest.Day.ToString("dd.MM.yyyy"),
                Filter = new DayExchangeRateFilter()
                {
                    Code = dayExchangeRateRequest.Code,
                    Country = dayExchangeRateRequest.Country,
                    CurrencyName = dayExchangeRateRequest.CurrencyName
                }
            };
            var result = await dayExchangeRateServiceService.GetData(exchangeRateParameter);
            return Ok(result);
        }
    }
}