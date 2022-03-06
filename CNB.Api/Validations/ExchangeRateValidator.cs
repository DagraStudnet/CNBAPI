using FluentValidation;

namespace CNB.Api.Validations
{
    public class ExchangeRateValidator<T> : AbstractValidator<T>
    {
        protected readonly DateTime start = new(1991, 1, 1);
        protected readonly DateTime end;

        protected ExchangeRateValidator()
        {
            end = DateTime.Today.AddDays(1).AddSeconds(-1);
        }
    }
}
