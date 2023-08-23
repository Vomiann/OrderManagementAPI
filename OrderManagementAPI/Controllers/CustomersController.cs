using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Mappers;
using OrderManagementAPI.Models;
using OrderManagementDAL.Services.Interfaces;

namespace OrderManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ResponseCustomer>> GetCustomers()
    {
        try
        {
            var customers = _customerService.GetAllCustomers();

            if (customers == null || !customers.Any())
            {
                return NotFound("No customers found.");
            }

            var responseCustomers = ApiMapper.MapToResponseCustomers(customers);

            return Ok(responseCustomers);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching customers.");
        }
    }

    [HttpPost]
    public ActionResult CreateCustomer(RequestCustomer requestCustomer)
    {
        try
        {
            var serviceCustomer = ApiMapper.MapToServiceCustomer(requestCustomer);

            _customerService.CreateCustomer(serviceCustomer);

            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the customer.");
        }
    }
}