using OrderManagementDAL.Services.Models;

namespace OrderManagementDAL.Services.Interfaces;

public interface ICustomerService
{
    List<ServiceCustomer> GetAllCustomers();
    void CreateCustomer(ServiceCustomer customer);
}