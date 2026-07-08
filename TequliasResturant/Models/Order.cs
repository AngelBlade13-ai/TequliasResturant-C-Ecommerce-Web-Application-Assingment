namespace TequliasResturant.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public string UserId { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }

    public ApplicationUser? User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
