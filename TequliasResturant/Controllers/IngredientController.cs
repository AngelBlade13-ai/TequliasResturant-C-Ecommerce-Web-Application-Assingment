using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TequliasResturant.Models;
using TequliasResturant.Models.Repositories;

namespace TequliasResturant.Controllers;

[Authorize]
public class IngredientController : Controller
{
    private readonly IRepository<Ingredient> _ingredients;

    public IngredientController(IRepository<Ingredient> ingredients)
    {
        _ingredients = ingredients;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var ingredients = await _ingredients.Query()
            .OrderBy(ingredient => ingredient.Name)
            .ToListAsync();

        return View(ingredients);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var ingredient = await _ingredients.Query()
            .Include(item => item.ProductIngredients)
                .ThenInclude(productIngredient => productIngredient.Product)
            .FirstOrDefaultAsync(item => item.IngredientId == id);

        return ingredient is null ? NotFound() : View(ingredient);
    }

    public IActionResult Create()
    {
        return View(new Ingredient());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ingredient ingredient)
    {
        if (!ModelState.IsValid)
        {
            return View(ingredient);
        }

        await _ingredients.AddAsync(ingredient);
        await _ingredients.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var ingredient = await _ingredients.GetByIdAsync(id);
        return ingredient is null ? NotFound() : View(ingredient);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Ingredient ingredient)
    {
        if (id != ingredient.IngredientId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(ingredient);
        }

        _ingredients.Update(ingredient);
        await _ingredients.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var ingredient = await _ingredients.Query()
            .Include(item => item.ProductIngredients)
                .ThenInclude(productIngredient => productIngredient.Product)
            .FirstOrDefaultAsync(item => item.IngredientId == id);

        return ingredient is null ? NotFound() : View(ingredient);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var ingredient = await _ingredients.Query()
            .Include(item => item.ProductIngredients)
            .FirstOrDefaultAsync(item => item.IngredientId == id);

        if (ingredient is null)
        {
            return NotFound();
        }

        if (ingredient.ProductIngredients.Count > 0)
        {
            ModelState.AddModelError(string.Empty, "Remove this ingredient from products before deleting it.");
            return View("Delete", ingredient);
        }

        _ingredients.Delete(ingredient);
        await _ingredients.SaveAsync();
        return RedirectToAction(nameof(Index));
    }
}
