using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using OrderManagementDAL.Services.Interfaces;
using OrderManagementDAL.Services.Models;
using System.Text;
using System.Text.Json;

namespace OrderManagementDAL.Services;

public class AzureBusService : IAzureBusService
{
    private readonly IConfiguration _configuration;

    private readonly string _serviceBusConnectionString;
    private readonly string _queueName;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public AzureBusService(IConfiguration configuration)
    {
        _configuration = configuration;

        _serviceBusConnectionString = _configuration.GetConnectionString("AzureServiceBusConnection");
        _queueName = _configuration["AzureServiceBusSettings:QueueName"];
    }

    public async Task SendMessageAsync(ServiceOrder serviceOrder)
    {
        await using var client = new ServiceBusClient(_serviceBusConnectionString);
        var sender = client.CreateSender(_queueName);

        var messageBody = JsonSerializer.Serialize(serviceOrder, _jsonSerializerOptions);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));

        await sender.SendMessageAsync(message);
    }
}