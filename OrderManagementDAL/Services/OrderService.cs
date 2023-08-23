using OrderManagementDAL.Models;
using OrderManagementDAL.Services.Interfaces;
using OrderManagementDAL.Services.Models;

namespace OrderManagementDAL.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationContext _dbContext;

    public OrderService(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ServiceOrder> GetAllOrders()
    {
        try
        {
            var orders = _dbContext.Orders.ToList();
            return Mapper.MapToServiceOrders(orders);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting orders.", ex);
        }
    }

    public ServiceOrder? GetOrderById(int orderId)
        {
        try
        {
            var orderEntity = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
            return orderEntity != null ? Mapper.MapToServiceOrder(orderEntity) : null;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching order details.", ex);
        }
    }

    public ServiceOrder CreateOrder(ServiceOrder serviceOrder)
    {
        try
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == serviceOrder.CustomerId);

            if (customer == null)
            {
                throw new InvalidOperationException($"Customer with CustomerId:[{serviceOrder.CustomerId}] not found.");
            }

            var model = Mapper.MapToOrder(serviceOrder, customer);

            _dbContext.Orders.Add(model);
            _dbContext.SaveChanges();

            return Mapper.MapToServiceOrder(model);

        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the order.", ex);
        }
    }

    public int UpdateOrder(int orderId, int orderStatus)
    {
        try
        {
            var orderEntity = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);

            if (orderEntity == null)
            {
                throw new InvalidOperationException($"Order with ID {orderId} not found.");
            }

            orderEntity.Status = (OrderStatus)orderStatus;
            _dbContext.SaveChanges();

            return orderEntity.Id;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the order status.", ex);
        }
    }
}