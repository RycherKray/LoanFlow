using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Cosmos;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        
        services.AddSingleton(serviceProvider =>
        {
            var connectionString = Environment.GetEnvironmentVariable("CosmosConn");
            return new CosmosClient(connectionString);
        });
    })
    .Build();

host.Run();
