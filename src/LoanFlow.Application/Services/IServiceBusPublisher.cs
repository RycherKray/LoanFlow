namespace LoanFlow.Application.Services;

public interface IServiceBusPublisher
{
    Task SendAsync(string topic, object payload, CancellationToken ct = default);
}
