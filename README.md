# Tequilas Restaurant

Tequilas Restaurant is an ASP.NET Core MVC restaurant e-commerce application. Customers can browse menu items, register or log in, add products to a session-based cart, place orders, and view their order history.

The app also includes management pages for products, categories, and ingredients, with Entity Framework Core handling the restaurant database relationships.

## Features

- ASP.NET Core MVC with Identity authentication
- Entity Framework Core with SQL Server LocalDB
- Product, category, ingredient, order, and order item models
- Many-to-many product ingredient relationships
- Generic repository pattern
- Product catalog with item photos
- Product/category/ingredient create, details, edit, and delete pages
- Session-based shopping cart
- Checkout with stock validation
- User order history

## Setup

1. Open `TequliasResturant.slnx` in Visual Studio.
2. Confirm SQL Server LocalDB is installed.
3. In Package Manager Console, run:

```powershell
Update-Database
```

4. Run the project.
5. Register a user account to test ordering, cart, checkout, and order history.

## Demo Suggestions

For a project demo or screenshots, show:

- Home page
- Product catalog
- Product details or edit page
- Shopping cart
- Checkout/order placement
- My Orders page
