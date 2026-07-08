using Microsoft.AspNetCore.Mvc.Rendering;
using TequliasResturant.Models;

namespace TequliasResturant.ViewModels;

public class ProductFormViewModel
{
    public Product Product { get; set; } = new();
    public List<int> SelectedIngredientIds { get; set; } = new();
    public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    public IEnumerable<SelectListItem> Ingredients { get; set; } = Enumerable.Empty<SelectListItem>();
}
