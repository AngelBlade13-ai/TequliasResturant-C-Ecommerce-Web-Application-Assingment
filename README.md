# Tequilas Restaurant

This is an ASP.NET Core MVC restaurant e-commerce app based on the tutorial video. It includes Identity login, Entity Framework Core models, a repository pattern, product/category/ingredient management, a session-based shopping cart, checkout, and user order history.

## Setup

1. Open `TequliasResturant.slnx` in Visual Studio.
2. Confirm SQL Server LocalDB is installed.
3. In Package Manager Console, run:

```powershell
Update-Database
```

4. Run the project.
5. Register a user, then try the product catalog, order page, cart, and my orders page.

## Feature Checklist

- [x] ASP.NET Core MVC project with Identity authentication
- [x] ApplicationUser model connected to orders
- [x] Product, Category, Ingredient, Order, OrderItem, and ProductIngredient models
- [x] EF Core DbContext with relationships and starter seed data
- [x] Generic repository pattern
- [x] Product catalog page
- [x] Category and ingredient management pages
- [x] Session-based cart
- [x] Place order and view orders flow
- [x] Full edit/details/delete CRUD pages for products, categories, and ingredients
- [x] Product ingredient management UI
- [x] Checkout with stock validation
- [ ] Screenshots or demo video for submission

## Submission Notes

Before submitting, make sure your GitHub repository is public and add screenshots or a short demo video showing the product catalog, cart, checkout, and order history.

## Tutorial Checkpoint Branches

Use these branches as working checkpoints while following the video:

- `tutorial/01-models-and-database`
- `tutorial/02-repository-and-crud`
- `tutorial/03-products-categories-ingredients`
- `tutorial/04-auth-and-navigation`
- `tutorial/05-cart-and-session`
- `tutorial/06-checkout-and-orders`
- `tutorial/07-final-polish-and-submission`

Example workflow:

```powershell
git switch tutorial/01-models-and-database
git add .
git commit -m "Complete models and database section"
git push origin tutorial/01-models-and-database
```

Then move to the next branch when you start the next section.
