namespace CNB.Api.Services;

public class ExchangeRateParameter<T>
{
    public string FileIdentifier { get; set; }
    public T Filter { get; set; }
}