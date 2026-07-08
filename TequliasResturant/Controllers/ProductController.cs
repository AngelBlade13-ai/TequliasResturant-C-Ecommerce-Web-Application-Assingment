using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TequliasResturant.Models;
using TequliasResturant.Models.Repositories;

namespace TequliasResturant.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly IRepository<Product> _products;

    public ProductController(IRepository<Product> products)
    {
        _products = products;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var products = await _products.GetAllAsync(product => product.Category!);
        return View(products);
    }

    public IActionResult Create()
    {
        return View(new Product());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        await _products.AddAsync(product);
        await _products.SaveAsync();
        return RedirectToAction(nameof(Index));
    }
}