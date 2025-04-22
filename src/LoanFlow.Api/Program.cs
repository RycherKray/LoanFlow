using LoanFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Azure.Messaging.ServiceBus;
using LoanFlow.Application.Commands;
using LoanFlow.Infrastructure.Services;
using LoanFlow.Application.Services;
using LoanFlow.Application.Repositories;
using System.Text.Json.Serialization;
using LoanFlow.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;
using LoanFlow.Application.Queries;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.Identity.Web;
using Microsoft.Extensions.DependencyInjection;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://icy-forest-0aa10540f.6.azurestaticapps.net")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

// Controllers
builder.Services
  .AddControllers()
  .AddJsonOptions(opts =>
    opts.JsonSerializerOptions.Converters
      .Add(new JsonStringEnumConverter()));

// EF Core + Azure SQL
builder.Services.AddDbContext<LoanDbContext>(opt =>
    opt.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString")));

// Repository
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

// Cosmos
builder.Services.AddSingleton(sp => new CosmosClient(Environment.GetEnvironmentVariable("CosmosConn")));
builder.Services.AddScoped<IProcessedLoanRepository, CosmosProcessedLoanRepository>();

// MediatR
builder.Services.AddMediatR(cfg =>
  cfg.RegisterServicesFromAssembly(typeof(CreateLoanRequestCommand).Assembly)
);

builder.Services.AddMediatR(cfg =>
  cfg.RegisterServicesFromAssembly(typeof(ListLoanRequestsQuery).Assembly)
);

// Service Bus
builder.Services.AddSingleton(sp =>
    new ServiceBusClient(Environment.GetEnvironmentVariable("ServiceBusConn")));
builder.Services.AddSingleton<IServiceBusPublisher, AzureServiceBusPublisher>();



var app = builder.Build();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();