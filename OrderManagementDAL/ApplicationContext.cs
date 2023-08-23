using Microsoft.EntityFrameworkCore;
using OrderManagementDAL.Models;

namespace OrderManagementDAL;

public class ApplicationContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
}