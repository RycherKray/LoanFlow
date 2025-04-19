using LoanFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Azure.Messaging.ServiceBus;
using LoanFlow.Application.Commands;
using LoanFlow.Infrastructure.Services;
using LoanFlow.Application.Services;
using LoanFlow.Application.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services
  .AddControllers()
  .AddJsonOptions(opts =>
    opts.JsonSerializerOptions.Converters
      .Add(new JsonStringEnumConverter()));

// EF Core + Azure SQL
builder.Services.AddDbContext<LoanDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Repository
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

// MediatR
builder.Services.AddMediatR(cfg =>
  cfg.RegisterServicesFromAssembly(typeof(CreateLoanRequestCommand).Assembly)
);

// Service Bus
builder.Services.AddSingleton(sp =>
    new ServiceBusClient(builder.Configuration["ServiceBus:Conn"]));
builder.Services.AddSingleton<IServiceBusPublisher, AzureServiceBusPublisher>();



var app = builder.Build();
app.MapControllers();
app.Run();