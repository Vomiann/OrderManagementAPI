using Azure.Messaging.ServiceBus;
using System.Text.Json;
using System.Text;
using ConsumerApp.Models;

class Program
{
    private const string _serviceBusConnectionString = "Endpoint=sb://testwebservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=5JuU7vf/pFgdG2j3HgIuPvYFSgfhmSebN+ASbBvyDjg=";
    private const string _queueName = "orderqueue";
    private static int _receivedOrderCount = 1;

    static async Task Main(string[] args)
    {
        await using var client = new ServiceBusClient(_serviceBusConnectionString);
        var processor = client.CreateProcessor(_queueName, new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false // Disable auto-completion to manually complete messages
        });

        processor.ProcessMessageAsync += ProcessMessageHandler;
        processor.ProcessErrorAsync += ProcessErrorHandler;

        Console.WriteLine("Press any key to stop receiving messages...");
        await processor.StartProcessingAsync();

        Console.ReadKey();

        Console.WriteLine("\nStopping the receiver...");
        await processor.StopProcessingAsync();
    }

    static async Task ProcessMessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        var serviceOrder = JsonSerializer.Deserialize<Order>(message);
        
        Console.WriteLine($"CustomerId:{serviceOrder.CustomerId}");
        Console.WriteLine($"receivedOrderCount:{_receivedOrderCount}");

        _receivedOrderCount++;

        // Mark the message as completed
        await args.CompleteMessageAsync(args.Message);
    }

    static Task ProcessErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"Error: {args.Exception.Message}");
        return Task.CompletedTask;
    }
}