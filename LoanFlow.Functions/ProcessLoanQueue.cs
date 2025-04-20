using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using LoanFlow.Application.Events;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using static Azure.Core.HttpHeader;

namespace LoanFlow.Functions
{
    public class ProcessLoanQueue
    {
        private readonly ILogger<ProcessLoanQueue> _logger;
        private readonly CosmosClient _cosmos;

        public ProcessLoanQueue(ILogger<ProcessLoanQueue> logger,
            CosmosClient cosmos
            )
        {
            _logger = logger;
            _cosmos = cosmos;
        }

        [Function(nameof(ProcessLoanQueue))]
        public async Task Run(
            [ServiceBusTrigger("loan-processing", Connection = "ServiceBusConn")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {

            try
            {
                var json = message.Body.ToString();
                _logger.LogInformation("🔔 Raw message body:\n{json}", json);

                var container = _cosmos
                    .GetDatabase("LoanFlow")
                    .GetContainer("ProcessedLoans");

                // Use messageId as Cosmos document id
                var doc = new
                {
                    id = message.MessageId,
                    raw = JsonSerializer.Deserialize<object>(json),
                    processedAt = DateTime.UtcNow
                };

                await container.CreateItemAsync(doc);

                _logger.LogInformation("✅ Message stored in Cosmos DB with ID: {id}", message.MessageId);
                await messageActions.CompleteMessageAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error saving message to Cosmos DB");
                throw; // Let Azure retry or dead-letter if needed
            }
        }
    }
}
