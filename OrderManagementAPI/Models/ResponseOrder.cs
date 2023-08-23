namespace OrderManagementAPI.Models;

public class ResponseOrder
{
    public int OrderId { get; set; }
    public string OrderName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderCost { get; set; }
    public string Description { get; set; }
    public ResponseOrderStatus Status { get; set; }
}