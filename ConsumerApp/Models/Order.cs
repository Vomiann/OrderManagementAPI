using System.Text.Json.Serialization;

namespace ConsumerApp.Models;

public class Order
{
    [JsonPropertyName("orderId")]
    public int OrderId { get; set; }

    [JsonPropertyName("orderName")]
    public string OrderName { get; set; }

    [JsonPropertyName("orderDate")]
    public DateTime OrderDate { get; set; }

    [JsonPropertyName("orderCost")]
    public decimal OrderCost { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("status")]
    public OrderStatus Status { get; set; }
    
    [JsonPropertyName("customerId")]
    public int CustomerId { get; set; }

    [JsonPropertyName("сustomer")]
    public Customer Customer { get; set; }
}