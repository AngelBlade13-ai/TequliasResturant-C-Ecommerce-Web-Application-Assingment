using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TequliasResturant.Models;
using TequliasResturant.Models.Repositories;
using TequliasResturant.ViewModels;

namespace TequliasResturant.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly IRepository<Category> _categories;
    private readonly IRepository<Ingredient> _ingredients;
    private readonly IRepository<Product> _products;

    public ProductController(
        IRepository<Product> products,
        IRepository<Category> categories,
        IRepository<Ingredient> ingredients)
    {
        _products = products;
        _categories = categories;
        _ingredients = ingredients;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var products = await _products.Query()
            .Include(product => product.Category)
            .Include(product => product.ProductIngredients)
                .ThenInclude(productIngredient => productIngredient.Ingredient)
            .OrderBy(product => product.Name)
            .ToListAsync();

        return View(products);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var product = await _products.Query()
            .Include(item => item.Category)
            .Include(item => item.ProductIngredients)
                .ThenInclude(productIngredient => productIngredient.Ingredient)
            .FirstOrDefaultAsync(item => item.ProductId == id);

        return product is null ? NotFound() : View(product);
    }

    public async Task<IActionResult> Create()
    {
        return View(await BuildProductFormAsync(new Product()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildProductFormAsync(model.Product, model.SelectedIngredientIds));
        }

        model.Product.ProductIngredients = model.SelectedIngredientIds
            .Select(ingredientId => new ProductIngredient { IngredientId = ingredientId })
            .ToList();

        await _products.AddAsync(model.Product);
        await _products.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _products.Query()
            .Include(item => item.ProductIngredients)
            .FirstOrDefaultAsync(item => item.ProductId == id);

        if (product is null)
        {
            return NotFound();
        }

        var selectedIngredientIds = product.ProductIngredients.Select(item => item.IngredientId);
        return View(await BuildProductFormAsync(product, selectedIngredientIds));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductFormViewModel model)
    {
        if (id != model.Product.ProductId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(await BuildProductFormAsync(model.Product, model.SelectedIngredientIds));
        }

        var product = await _products.Query()
            .Include(item => item.ProductIngredients)
            .FirstOrDefaultAsync(item => item.ProductId == id);

        if (product is null)
        {
            return NotFound();
        }

        product.Name = model.Product.Name;
        product.Description = model.Product.Description;
        product.Price = model.Product.Price;
        product.Stock = model.Product.Stock;
        product.ImageUrl = model.Product.ImageUrl;
        product.CategoryId = model.Product.CategoryId;

        product.ProductIngredients.Clear();
        foreach (var ingredientId in model.SelectedIngredientIds)
        {
            product.ProductIngredients.Add(new ProductIngredient
            {
                ProductId = product.ProductId,
                IngredientId = ingredientId
            });
        }

        _products.Update(product);
        await _products.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _products.Query()
            .Include(item => item.Category)
            .Include(item => item.ProductIngredients)
                .ThenInclude(productIngredient => productIngredient.Ingredient)
            .FirstOrDefaultAsync(item => item.ProductId == id);

        return product is null ? NotFound() : View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _products.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        _products.Delete(product);
        await _products.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<ProductFormViewModel> BuildProductFormAsync(Product product, IEnumerable<int>? selectedIngredientIds = null)
    {
        var categories = await _categories.GetAllAsync();
        var ingredients = await _ingredients.GetAllAsync();
        var selected = selectedIngredientIds?.ToList() ?? new List<int>();

        return new ProductFormViewModel
        {
            Product = product,
            SelectedIngredientIds = selected,
            Categories = categories
                .OrderBy(category => category.Name)
                .Select(category => new SelectListItem(category.Name, category.CategoryId.ToString())),
            Ingredients = ingredients
                .OrderBy(ingredient => ingredient.Name)
                .Select(ingredient => new SelectListItem(ingredient.Name, ingredient.IngredientId.ToString(), selected.Contains(ingredient.IngredientId)))
        };
    }
}
