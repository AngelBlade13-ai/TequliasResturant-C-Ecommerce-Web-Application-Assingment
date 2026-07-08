using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        return View(await _ingredients.GetAllAsync());
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
}
