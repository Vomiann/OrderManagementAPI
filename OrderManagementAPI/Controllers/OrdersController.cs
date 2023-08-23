using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Mappers;
using OrderManagementAPI.Models;
using OrderManagementDAL.Services.Interfaces;

namespace OrderManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IAzureBusService _azureBusService;

    public OrdersController(IOrderService orderService, IAzureBusService azureBusService)
    {
        _orderService = orderService;
        _azureBusService = azureBusService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ResponseOrder>> GetOrders()
    {
        try
        {
            var serviceOrders = _orderService.GetAllOrders();

            if (serviceOrders == null || !serviceOrders.Any())
            {
                return NotFound("No orders found.");
            }

            var responseOrders = ApiMapper.MapToResponseOrders(serviceOrders);

            return Ok(responseOrders);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching orders.");
        }
    }

    [HttpGet("{orderId}")]
    public ActionResult<ResponseOrder> GetOrderById(int orderId)
    {
        try
        {
            var serviceOrder = _orderService.GetOrderById(orderId);

            if (serviceOrder == null)
            {
                return NotFound($"Order with ID {orderId} not found.");
            }

            return Ok(ApiMapper.MapToResponseOrder(serviceOrder));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while fetching Order with ID {orderId}.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder(RequestOrder requestOrder)
    {
        try
        {
            var serviceOrder = ApiMapper.MapToServiceOrder(requestOrder);

            var resultServiceOrder = _orderService.CreateOrder(serviceOrder);

            await _azureBusService.SendMessageAsync(resultServiceOrder);

            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the order.");
        }
    }

    [HttpPatch("{orderId}")]
    public ActionResult<int> UpdateOrderStatus(int orderId, [FromBody] int orderStatus)
    {
        try
        {
            var updatedOrderId = _orderService.UpdateOrder(orderId, orderStatus);
            return Ok(updatedOrderId);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound($"Order with ID {orderId} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"An error occurred while updating Order with ID {orderId}.");
        }
    }
}