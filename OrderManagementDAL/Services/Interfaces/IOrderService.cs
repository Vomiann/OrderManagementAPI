using OrderManagementDAL.Services.Models;

namespace OrderManagementDAL.Services.Interfaces;

public interface IOrderService
{
    List<ServiceOrder> GetAllOrders();
    ServiceOrder GetOrderById(int orderId);
    ServiceOrder CreateOrder(ServiceOrder order);
    int UpdateOrder(int orderId, int orderStatus);
}