using OrderManagementDAL.Models;

namespace OrderManagementAPI.Models;

public class RequestOrder
{
    public string OrderName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderCost { get; set; }
    public string Description { get; set; }
    public OrderStatus Status { get; set; }
    public int CustomerId { get; set; }
}