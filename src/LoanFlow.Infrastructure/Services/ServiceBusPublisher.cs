using Azure.Messaging.ServiceBus;
using System.Text.Json;
using LoanFlow.Application.Services; // interface
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace LoanFlow.Infrastructure.Services;

public class AzureServiceBusPublisher : IServiceBusPublisher, IAsyncDisposable
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _primarySender;
    private readonly ServiceBusSender _errorSender;
    private readonly ILogger<AzureServiceBusPublisher> _logger;

    public AzureServiceBusPublisher(
        ServiceBusClient client,
        IConfiguration config,
        ILogger<AzureServiceBusPublisher> logger)
    {
        _client = client;
        _primarySender = client.CreateSender(config["ServiceBus:PrimaryQueue"]);    // e.g. "loan-processing"
        _errorSender = client.CreateSender(config["ServiceBus:ErrorQueue"]);      // e.g. "loan-log-errors"
        _logger = logger;
    }

    public async Task SendAsync(string topic, object payload, CancellationToken ct = default)
    {
        // Serialize once
        var body = JsonSerializer.Serialize(payload);
        var msg = new ServiceBusMessage(body);

        const int maxAttempts = 3;
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                await _primarySender.SendMessageAsync(msg, ct);
                return; // success! exit method
            }
            catch (Exception ex) when (attempt < maxAttempts)
            {
                _logger.LogWarning(ex, "Attempt {Attempt} to send message failed, retrying...", attempt);
                await Task.Delay(TimeSpan.FromSeconds(2 * attempt), ct); // linear back‑off
            }
        }

        // All retries failed → log to error queue
        var errorPayload = new
        {
            Error = "Failed to publish to primary queue after 3 attempts",
            Exception = true,
            TimestampUtc = DateTime.UtcNow,
            Original = payload
        };

        try
        {
            var errorMsg = new ServiceBusMessage(JsonSerializer.Serialize(errorPayload));
            await _errorSender.SendMessageAsync(errorMsg, ct);
            _logger.LogError("Published failure notice to error queue");
        }
        catch (Exception finalEx)
        {
            _logger.LogError(finalEx, "Failed to publish to error queue as well");            
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _primarySender.DisposeAsync();
        await _errorSender.DisposeAsync();
        await _client.DisposeAsync();
    }
}
