using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        var categories = await _categories.Query()
            .Include(category => category.Products)
            .OrderBy(category => category.Name)
            .ToListAsync();

        return View(categories);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var category = await _categories.Query()
            .Include(item => item.Products)
            .FirstOrDefaultAsync(item => item.CategoryId == id);

        return category is null ? NotFound() : View(category);
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

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categories.GetByIdAsync(id);
        return category is null ? NotFound() : View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(category);
        }

        _categories.Update(category);
        await _categories.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categories.Query()
            .Include(item => item.Products)
            .FirstOrDefaultAsync(item => item.CategoryId == id);

        return category is null ? NotFound() : View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _categories.Query()
            .Include(item => item.Products)
            .FirstOrDefaultAsync(item => item.CategoryId == id);

        if (category is null)
        {
            return NotFound();
        }

        if (category.Products.Count > 0)
        {
            ModelState.AddModelError(string.Empty, "Move or delete products in this category before deleting it.");
            return View("Delete", category);
        }

        _categories.Delete(category);
        await _categories.SaveAsync();
        return RedirectToAction(nameof(Index));
    }
}
