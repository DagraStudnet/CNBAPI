using CNB.Api.Requests;
using FluentValidation;

namespace CNB.Api.Validations
{
    public class YearExchangeRateRequestValidator : ExchangeRateValidator<YearExchangeRateRequest>
    {
        public YearExchangeRateRequestValidator()
        {
            RuleFor(x => x.Year)
                .NotEmpty()
                .GreaterThanOrEqualTo(x => start.Year)
                .WithMessage($"Year must be great or equal {start.Year}")
                .LessThanOrEqualTo(x => end.Year)
                .WithMessage($"Day must be less or equal {end.Year}");

            RuleFor(x => x.Day)
                .GreaterThanOrEqualTo(x => start)
                .WithMessage($"Year must be great or equal {start:dd.MM.yyyy} date")
                .LessThanOrEqualTo(x => end)
                .WithMessage($"Day must be less or equal {end:dd.MM.yyyy} date");
        }
    }
}
