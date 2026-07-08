# Tequilas Restaurant

This is an ASP.NET Core MVC restaurant e-commerce skeleton built to follow along with the tutorial video. It includes the project structure for Identity login, Entity Framework Core models, a repository pattern, product/category/ingredient pages, a session-based shopping cart, and order history.

The goal is to save setup time without fully completing the assignment. Several pages are intentionally simple so they can be improved while following the video.

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
- [x] Generic repository skeleton
- [x] Product catalog page
- [x] Category and ingredient starter pages
- [x] Session-based cart starter
- [x] Place order and view orders starter flow
- [ ] Full edit/details/delete CRUD pages
- [ ] Product ingredient management UI
- [ ] Polished checkout experience
- [ ] Screenshots or demo video for submission

## Notes For Follow-Along Work

The create forms are intentionally plain. For example, `Product/Create` uses a raw `CategoryId` input instead of a dropdown. That leaves room to follow the tutorial sections on view models, select lists, and richer CRUD pages.

Before submitting, make regular commits with descriptive messages and add screenshots showing the product catalog, cart, checkout, and order history.
