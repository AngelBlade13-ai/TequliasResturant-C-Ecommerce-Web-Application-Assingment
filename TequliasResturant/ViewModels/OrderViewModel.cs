using TequliasResturant.Models;

namespace TequliasResturant.ViewModels;

public class OrderViewModel
{
    public List<Product> Products { get; set; } = new();
    public List<OrderItemViewModel> OrderItems { get; set; } = new();
    public decimal TotalAmount { get; set; }
}
