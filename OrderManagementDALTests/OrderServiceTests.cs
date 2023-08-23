using Microsoft.EntityFrameworkCore;
using Moq;
using OrderManagementDAL;
using OrderManagementDAL.Models;
using OrderManagementDAL.Services;
using OrderManagementDAL.Services.Models;
using Xunit;

namespace OrderManagementDALTests;

public class OrderServiceTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void GetOrderById_ValidOrderId_ShouldReturnServiceOrder(int orderId)
    {
        using (var context = CreateDbContext())
        {
            // Arrange
            var orderEntity = new Order { Id = orderId, Description = "Order Description", OrderName = "Order Name" };
            context.Orders.Add(orderEntity);
            context.SaveChanges();

            var orderService = new OrderService(context);

            // Act
            var result = orderService.GetOrderById(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
        }
    }

    [Fact]
    public void GetOrderById_InvalidOrderId_ShouldReturnNull()
    {
        using (var context = CreateDbContext())
        {
            // Arrange
            var orderService = new OrderService(context);

            // Act
            var result = orderService.GetOrderById(1);

            // Assert
            Assert.Null(result);
        }
    }

    [Fact]
    public void CreateOrder_ValidServiceOrder_ShouldReturnServiceOrder()
    {
        using (var context = CreateDbContext())
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Test Customer" };
            context.Customers.Add(customer);
            context.SaveChanges();

            var orderService = new OrderService(context);

            var serviceOrder = new ServiceOrder
            {
                CustomerId = 1,
                Description = "Order Description",
                OrderName = "Order Name"
            };

            // Act
            var result = orderService.CreateOrder(serviceOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CustomerId);
        }
    }

    [Fact]
    public void CreateOrder_InvalidCustomerId_ShouldThrowException()
    {
        using (var context = CreateDbContext())
        {
            // Arrange
            var orderService = new OrderService(context);

            var serviceOrder = new ServiceOrder
            {
                CustomerId = 1, // Assuming customer with ID 1 does not exist
                Description = "Order Description",
                OrderName = "Order Name"
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => orderService.CreateOrder(serviceOrder));
            Assert.Equal("An error occurred while creating the order.", exception.Message);
        }
    }

    [Fact]
    public void GetAllOrders_ShouldReturnListOfServiceOrders()
    {
        using (var context = CreateDbContext())
        {
            // Arrange
            var orders = new List<Order>
                {
                    new Order { Id = 1, Description = "Order 1 Description", OrderName = "Order 1 Name" },
                    new Order { Id = 2, Description = "Order 2 Description", OrderName = "Order 2 Name" }
                };

            context.Orders.AddRange(orders);
            context.SaveChanges();

            var orderService = new OrderService(context);

            // Act
            var result = orderService.GetAllOrders();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].OrderId);
            Assert.Equal(2, result[1].OrderId);
        }
    }

    private ApplicationContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationContext(options);
    }
}