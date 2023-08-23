namespace OrderManagementDAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderCost { get; set; }
        public string Description { get; set; }
        public OrderStatus Status { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
