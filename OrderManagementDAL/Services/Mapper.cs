using OrderManagementDAL.Models;
using OrderManagementDAL.Services.Models;

namespace OrderManagementDAL.Services;

public static class Mapper
{
    public static List<ServiceCustomer> MapToServiceCustomer(List<Customer> customers)
    {
        return customers.Select(s => new ServiceCustomer()
        {
            CustomerId = s.Id,
            Name = s.Name,
            RegistrationDate = s.RegistrationDate
        }).ToList();
    }

    public static Customer MapToCustomer(ServiceCustomer customer)
    {
        return new Customer()
        {
            Id = customer.CustomerId,
            Name = customer.Name,
            RegistrationDate = customer.RegistrationDate
        };
    }

    public static List<ServiceOrder> MapToServiceOrders(List<Order> orders)
    {
        return orders.Select(o => new ServiceOrder()
        {
            OrderId = o.Id,
            OrderName = o.OrderName,
            Description = o.Description,
            Status = (ServiceOrderStatus)o.Status,
            OrderCost = o.OrderCost,
            OrderDate = o.OrderDate
        }).ToList();
    }

    public static ServiceOrder MapToServiceOrder(Order order)
    {
        return new ServiceOrder()
        {
            OrderId = order.Id,
            OrderName = order.OrderName,
            Description = order.Description,
            Status = (ServiceOrderStatus) order.Status,
            OrderCost = order.OrderCost,
            OrderDate = order.OrderDate,
            CustomerId = order.CustomerId
        };
    }

    public static Order MapToOrder(ServiceOrder serviceOrder, Customer customer)
    {
        return new Order()
        {
            CustomerId = serviceOrder.CustomerId,
            OrderName = serviceOrder.OrderName,
            Description = serviceOrder.Description,
            Status = (OrderStatus)serviceOrder.Status,
            OrderCost = serviceOrder.OrderCost,
            OrderDate = serviceOrder.OrderDate,
            Customer = customer
        };
    }
}