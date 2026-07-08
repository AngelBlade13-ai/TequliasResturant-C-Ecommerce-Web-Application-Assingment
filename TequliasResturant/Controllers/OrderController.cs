using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TequliasResturant.Extensions;
using TequliasResturant.Models;
using TequliasResturant.Models.Repositories;
using TequliasResturant.ViewModels;

namespace TequliasResturant.Controllers;

[Authorize]
public class OrderController : Controller
{
    private const string CartSessionKey = "OrderViewModel";
    private readonly IRepository<Order> _orders;
    private readonly IRepository<Product> _products;

    public OrderController(IRepository<Order> orders, IRepository<Product> products)
    {
        _orders = orders;
        _products = products;
    }

    public async Task<IActionResult> Create()
    {
        var model = HttpContext.Session.GetObject<OrderViewModel>(CartSessionKey) ?? new OrderViewModel();
        model.Products = (await _products.GetAllAsync(product => product.Category!)).ToList();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(int prodId, int prodQty)
    {
        var product = await _products.GetByIdAsync(prodId);
        if (product is null)
        {
            return NotFound();
        }

        var model = HttpContext.Session.GetObject<OrderViewModel>(CartSessionKey) ?? new OrderViewModel();
        var existingItem = model.OrderItems.FirstOrDefault(item => item.ProductId == prodId);

        if (existingItem is null)
        {
            model.OrderItems.Add(new OrderItemViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Quantity = Math.Max(prodQty, 1),
                Price = product.Price
            });
        }
        else
        {
            existingItem.Quantity += Math.Max(prodQty, 1);
        }

        model.TotalAmount = model.OrderItems.Sum(item => item.Price * item.Quantity);
        HttpContext.Session.SetObject(CartSessionKey, model);
        return RedirectToAction(nameof(Create));
    }

    public IActionResult Cart()
    {
        var model = HttpContext.Session.GetObject<OrderViewModel>(CartSessionKey);
        if (model is null || model.OrderItems.Count == 0)
        {
            return RedirectToAction(nameof(Create));
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PlaceOrder()
    {
        var model = HttpContext.Session.GetObject<OrderViewModel>(CartSessionKey);
        if (model is null || model.OrderItems.Count == 0)
        {
            return RedirectToAction(nameof(Create));
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return Challenge();
        }

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            TotalAmount = model.TotalAmount,
            OrderItems = model.OrderItems.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
        };

        await _orders.AddAsync(order);
        await _orders.SaveAsync();
        HttpContext.Session.Remove(CartSessionKey);
        return RedirectToAction(nameof(ViewOrders));
    }

    public async Task<IActionResult> ViewOrders()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return Challenge();
        }

        var orders = await _orders.GetAllByIdAsync(userId, nameof(Order.UserId), order => order.OrderItems);
        return View(orders);
    }
}
