using OrderManagementDAL.Models;

namespace OrderManagementDAL.Services.Models;

public class ServiceOrder
{
    public int OrderId { get; set; }
    public string OrderName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderCost { get; set; }
    public string Description { get; set; }
    public ServiceOrderStatus Status { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}