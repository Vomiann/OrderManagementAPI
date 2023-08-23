using OrderManagementDAL.Services.Interfaces;
using OrderManagementDAL.Services.Models;

namespace OrderManagementDAL.Services;

public class CustomerService : ICustomerService
{
    private readonly ApplicationContext _dbContext;

    public CustomerService(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ServiceCustomer> GetAllCustomers()
    {
        try
        {
            var customers = _dbContext.Customers.ToList();
            return Mapper.MapToServiceCustomer(customers);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting customers.", ex);
        }
    }

    public void CreateCustomer(ServiceCustomer serviceCustomer)
    {
        try
        {
            var model = Mapper.MapToCustomer(serviceCustomer);

            _dbContext.Customers.Add(model);
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the customer.", ex);
        }
    }
}