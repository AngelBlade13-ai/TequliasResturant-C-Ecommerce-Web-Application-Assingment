using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TequliasResturant.Models;
using TequliasResturant.Models.Repositories;

namespace TequliasResturant.Controllers;

[Authorize]
public class CategoryController : Controller
{
    private readonly IRepository<Category> _categories;

    public CategoryController(IRepository<Category> categories)
    {
        _categories = categories;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await _categories.GetAllAsync(category => category.Products));
    }

    public IActionResult Create()
    {
        return View(new Category());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        await _categories.AddAsync(category);
        await _categories.SaveAsync();
        return RedirectToAction(nameof(Index));
    }
}