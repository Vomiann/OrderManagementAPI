using OrderManagementAPI.Models;
using OrderManagementDAL.Services.Models;

namespace OrderManagementAPI.Mappers;

public static class ApiMapper
{
    public static ServiceCustomer MapToServiceCustomer(RequestCustomer requestCustomer)
    {
        return new ServiceCustomer()
        {
            Name = requestCustomer.Name,
            RegistrationDate = requestCustomer.RegistrationDate
        };
    }

    public static List<ResponseCustomer> MapToResponseCustomers(List<ServiceCustomer> serviceCustomers)
    {
        return serviceCustomers.Select(sc => new ResponseCustomer()
        {
            CustomerId = sc.CustomerId,
            Name = sc.Name,
            RegistrationDate = sc.RegistrationDate
        }).ToList();
    }
    public static List<ResponseOrder> MapToResponseOrders(List<ServiceOrder> serviceOrders)
    {
        return serviceOrders.Select(so => new ResponseOrder()
        {
            OrderId = so.OrderId,
            OrderName = so.OrderName,
            Status = (ResponseOrderStatus)so.Status,
            Description = so.Description,
            OrderCost = so.OrderCost,
            OrderDate = so.OrderDate
        }).ToList();
    }

    public static ResponseOrder MapToResponseOrder(ServiceOrder serviceOrder)
    {
        return new ResponseOrder()
        {
            OrderId = serviceOrder.OrderId,
            OrderName = serviceOrder.OrderName,
            Description = serviceOrder.Description,
            Status = (ResponseOrderStatus)serviceOrder.Status,
            OrderCost = serviceOrder.OrderCost,
            OrderDate = serviceOrder.OrderDate
        };
    }

    public static ServiceOrder MapToServiceOrder(RequestOrder requestOrder)
    {
        return new ServiceOrder()
        {
            CustomerId = requestOrder.CustomerId,
            OrderName = requestOrder.OrderName,
            Description = requestOrder.Description,
            OrderCost = requestOrder.OrderCost,
            Status = (ServiceOrderStatus)requestOrder.Status,
            OrderDate = requestOrder.OrderDate
        };
    }
}