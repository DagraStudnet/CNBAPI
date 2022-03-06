using CNB.Api.Requests;
using FluentValidation;

namespace CNB.Api.Validations
{
    public class DayExchangeRateRequestValidator: ExchangeRateValidator<DayExchangeRateRequest>
    {
        public DayExchangeRateRequestValidator()
        {
            RuleFor(x => x.Day)
                .NotEmpty()
                .GreaterThanOrEqualTo(x => start)
                .WithMessage($"Day must be great or equal {start:dd.MM.yyyy} date")
                .LessThanOrEqualTo(x => end)
                .WithMessage($"Day must be less or equal {end:dd.MM.yyyy} date");

        }
    }
}
