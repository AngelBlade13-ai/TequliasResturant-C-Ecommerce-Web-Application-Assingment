using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TequliasResturant.Models;

namespace TequliasResturant.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductIngredient> ProductIngredients => Set<ProductIngredient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define composite key and relationships for ProductIngredient

        modelBuilder.Entity<ProductIngredient>()
            .HasKey(productIngredient => new { productIngredient.ProductId, productIngredient.IngredientId });

        modelBuilder.Entity<ProductIngredient>()
            .HasOne(productIngredient => productIngredient.Product)
            .WithMany(product => product.ProductIngredients)
            .HasForeignKey(productIngredient => productIngredient.ProductId);

        modelBuilder.Entity<ProductIngredient>()
            .HasOne(productIngredient => productIngredient.Ingredient)
            .WithMany(ingredient => ingredient.ProductIngredients)
            .HasForeignKey(productIngredient => productIngredient.IngredientId);

        //

        modelBuilder.Entity<Product>()
            .Property(product => product.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(orderItem => orderItem.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(order => order.TotalAmount)
            .HasPrecision(18, 2);

        //Seed Data

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Appetizers", Description = "Shareable starters and snacks." },
            new Category { CategoryId = 2, Name = "Entrees", Description = "Tacos, enchiladas, burritos, and house plates." },
            new Category { CategoryId = 3, Name = "Sides", Description = "Small extras to round out a meal." },
            new Category { CategoryId = 4, Name = "Desserts", Description = "Sweet finishes from the kitchen." },
            new Category { CategoryId = 5, Name = "Beverages", Description = "Refreshing drinks and aguas frescas." });

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, Name = "Beef Street Tacos", Description = "Three corn tortilla tacos with seasoned beef, onion, cilantro, salsa roja, and lime.", Price = 8.99m, Stock = 65, CategoryId = 2, ImageUrl = "/images/menu/tacos.png" },
            new Product { ProductId = 2, Name = "Chicken Tacos", Description = "Grilled chicken tacos with lettuce, pico de gallo, crema, and cotija cheese.", Price = 8.49m, Stock = 70, CategoryId = 2, ImageUrl = "/images/menu/tacos.png" },
            new Product { ProductId = 3, Name = "Baja Fish Tacos", Description = "Crispy fish tacos with cabbage slaw, lime crema, pico de gallo, and salsa verde.", Price = 9.99m, Stock = 45, CategoryId = 2, ImageUrl = "/images/menu/tacos.png" },
            new Product { ProductId = 4, Name = "Chicken Enchiladas", Description = "Two chicken enchiladas covered in red chile sauce, melted cheese, crema, rice, and beans.", Price = 11.99m, Stock = 40, CategoryId = 2, ImageUrl = "/images/menu/enchiladas.png" },
            new Product { ProductId = 5, Name = "Veggie Enchiladas", Description = "Cheese and roasted vegetable enchiladas with salsa verde, crema, rice, and beans.", Price = 10.99m, Stock = 35, CategoryId = 2, ImageUrl = "/images/menu/enchiladas.png" },
            new Product { ProductId = 6, Name = "Carne Asada Burrito", Description = "Large flour tortilla filled with steak, rice, beans, cheese, pico, guacamole, and salsa.", Price = 12.49m, Stock = 38, CategoryId = 2, ImageUrl = "/images/menu/tacos.png" },
            new Product { ProductId = 7, Name = "Loaded Nachos", Description = "Tortilla chips with queso, beans, pico, jalapenos, sour cream, guacamole, and salsa.", Price = 9.49m, Stock = 55, CategoryId = 1, ImageUrl = "/images/menu/nachos.png" },
            new Product { ProductId = 8, Name = "Chicken Quesadilla", Description = "Grilled flour tortilla with chicken and melted cheese, served with salsa and crema.", Price = 8.99m, Stock = 50, CategoryId = 1, ImageUrl = "/images/menu/quesadilla.png" },
            new Product { ProductId = 9, Name = "Chips and Salsa Trio", Description = "Warm tortilla chips with salsa roja, salsa verde, and pico de gallo.", Price = 5.99m, Stock = 80, CategoryId = 1, ImageUrl = "/images/menu/nachos.png" },
            new Product { ProductId = 10, Name = "Mexican Rice", Description = "Savory tomato rice with garlic, onion, and cilantro.", Price = 3.49m, Stock = 90, CategoryId = 3, ImageUrl = "/images/menu/enchiladas.png" },
            new Product { ProductId = 11, Name = "Refried Beans", Description = "Creamy pinto beans finished with cotija cheese.", Price = 3.49m, Stock = 85, CategoryId = 3, ImageUrl = "/images/menu/enchiladas.png" },
            new Product { ProductId = 12, Name = "Churros", Description = "Cinnamon sugar churros served with warm chocolate dipping sauce.", Price = 5.49m, Stock = 45, CategoryId = 4, ImageUrl = "/images/menu/churros.png" },
            new Product { ProductId = 13, Name = "Horchata", Description = "Creamy cinnamon rice drink served cold.", Price = 3.49m, Stock = 60, CategoryId = 5, ImageUrl = "/images/menu/drinks.png" },
            new Product { ProductId = 14, Name = "Agua Fresca Flight", Description = "Three small seasonal aguas frescas: jamaica, lime, and horchata.", Price = 5.99m, Stock = 40, CategoryId = 5, ImageUrl = "/images/menu/drinks.png" });

        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { IngredientId = 1, Name = "Beef", Description = "Seasoned ground beef or steak." },
            new Ingredient { IngredientId = 2, Name = "Chicken", Description = "Grilled or shredded chicken." },
            new Ingredient { IngredientId = 3, Name = "Fish", Description = "Crispy white fish." },
            new Ingredient { IngredientId = 4, Name = "Corn Tortilla", Description = "Soft corn tortilla." },
            new Ingredient { IngredientId = 5, Name = "Flour Tortilla", Description = "Large grilled flour tortilla." },
            new Ingredient { IngredientId = 6, Name = "Lettuce", Description = "Crisp shredded lettuce." },
            new Ingredient { IngredientId = 7, Name = "Tomato", Description = "Fresh tomato for pico and toppings." },
            new Ingredient { IngredientId = 8, Name = "Cilantro", Description = "Fresh chopped cilantro." },
            new Ingredient { IngredientId = 9, Name = "Onion", Description = "Diced white onion." },
            new Ingredient { IngredientId = 10, Name = "Cheese", Description = "Melted cheese or cotija." },
            new Ingredient { IngredientId = 11, Name = "Crema", Description = "Mexican crema." },
            new Ingredient { IngredientId = 12, Name = "Guacamole", Description = "Fresh avocado guacamole." },
            new Ingredient { IngredientId = 13, Name = "Salsa Roja", Description = "Red chile salsa." },
            new Ingredient { IngredientId = 14, Name = "Salsa Verde", Description = "Green tomatillo salsa." },
            new Ingredient { IngredientId = 15, Name = "Rice", Description = "Mexican rice." },
            new Ingredient { IngredientId = 16, Name = "Beans", Description = "Pinto or refried beans." },
            new Ingredient { IngredientId = 17, Name = "Tortilla Chips", Description = "Crispy corn chips." },
            new Ingredient { IngredientId = 18, Name = "Jalapeno", Description = "Sliced jalapenos." },
            new Ingredient { IngredientId = 19, Name = "Cinnamon", Description = "Warm cinnamon spice." },
            new Ingredient { IngredientId = 20, Name = "Chocolate", Description = "Chocolate dipping sauce." },
            new Ingredient { IngredientId = 21, Name = "Lime", Description = "Fresh lime wedges or juice." },
            new Ingredient { IngredientId = 22, Name = "Horchata", Description = "Cinnamon rice drink base." },
            new Ingredient { IngredientId = 23, Name = "Jamaica", Description = "Hibiscus agua fresca." });

        modelBuilder.Entity<ProductIngredient>().HasData(
            new ProductIngredient { ProductId = 1, IngredientId = 1 },
            new ProductIngredient { ProductId = 1, IngredientId = 4 },
            new ProductIngredient { ProductId = 1, IngredientId = 8 },
            new ProductIngredient { ProductId = 1, IngredientId = 9 },
            new ProductIngredient { ProductId = 1, IngredientId = 13 },
            new ProductIngredient { ProductId = 1, IngredientId = 21 },
            new ProductIngredient { ProductId = 2, IngredientId = 2 },
            new ProductIngredient { ProductId = 2, IngredientId = 4 },
            new ProductIngredient { ProductId = 2, IngredientId = 6 },
            new ProductIngredient { ProductId = 2, IngredientId = 7 },
            new ProductIngredient { ProductId = 2, IngredientId = 10 },
            new ProductIngredient { ProductId = 2, IngredientId = 11 },
            new ProductIngredient { ProductId = 3, IngredientId = 3 },
            new ProductIngredient { ProductId = 3, IngredientId = 4 },
            new ProductIngredient { ProductId = 3, IngredientId = 6 },
            new ProductIngredient { ProductId = 3, IngredientId = 11 },
            new ProductIngredient { ProductId = 3, IngredientId = 14 },
            new ProductIngredient { ProductId = 3, IngredientId = 21 },
            new ProductIngredient { ProductId = 4, IngredientId = 2 },
            new ProductIngredient { ProductId = 4, IngredientId = 4 },
            new ProductIngredient { ProductId = 4, IngredientId = 10 },
            new ProductIngredient { ProductId = 4, IngredientId = 11 },
            new ProductIngredient { ProductId = 4, IngredientId = 13 },
            new ProductIngredient { ProductId = 4, IngredientId = 15 },
            new ProductIngredient { ProductId = 4, IngredientId = 16 },
            new ProductIngredient { ProductId = 5, IngredientId = 4 },
            new ProductIngredient { ProductId = 5, IngredientId = 10 },
            new ProductIngredient { ProductId = 5, IngredientId = 11 },
            new ProductIngredient { ProductId = 5, IngredientId = 14 },
            new ProductIngredient { ProductId = 5, IngredientId = 15 },
            new ProductIngredient { ProductId = 5, IngredientId = 16 },
            new ProductIngredient { ProductId = 6, IngredientId = 1 },
            new ProductIngredient { ProductId = 6, IngredientId = 5 },
            new ProductIngredient { ProductId = 6, IngredientId = 7 },
            new ProductIngredient { ProductId = 6, IngredientId = 10 },
            new ProductIngredient { ProductId = 6, IngredientId = 12 },
            new ProductIngredient { ProductId = 6, IngredientId = 13 },
            new ProductIngredient { ProductId = 6, IngredientId = 15 },
            new ProductIngredient { ProductId = 6, IngredientId = 16 },
            new ProductIngredient { ProductId = 7, IngredientId = 10 },
            new ProductIngredient { ProductId = 7, IngredientId = 12 },
            new ProductIngredient { ProductId = 7, IngredientId = 13 },
            new ProductIngredient { ProductId = 7, IngredientId = 16 },
            new ProductIngredient { ProductId = 7, IngredientId = 17 },
            new ProductIngredient { ProductId = 7, IngredientId = 18 },
            new ProductIngredient { ProductId = 8, IngredientId = 2 },
            new ProductIngredient { ProductId = 8, IngredientId = 5 },
            new ProductIngredient { ProductId = 8, IngredientId = 10 },
            new ProductIngredient { ProductId = 8, IngredientId = 11 },
            new ProductIngredient { ProductId = 8, IngredientId = 13 },
            new ProductIngredient { ProductId = 9, IngredientId = 7 },
            new ProductIngredient { ProductId = 9, IngredientId = 13 },
            new ProductIngredient { ProductId = 9, IngredientId = 14 },
            new ProductIngredient { ProductId = 9, IngredientId = 17 },
            new ProductIngredient { ProductId = 10, IngredientId = 8 },
            new ProductIngredient { ProductId = 10, IngredientId = 9 },
            new ProductIngredient { ProductId = 10, IngredientId = 15 },
            new ProductIngredient { ProductId = 11, IngredientId = 10 },
            new ProductIngredient { ProductId = 11, IngredientId = 16 },
            new ProductIngredient { ProductId = 12, IngredientId = 19 },
            new ProductIngredient { ProductId = 12, IngredientId = 20 },
            new ProductIngredient { ProductId = 13, IngredientId = 19 },
            new ProductIngredient { ProductId = 13, IngredientId = 22 },
            new ProductIngredient { ProductId = 14, IngredientId = 21 },
            new ProductIngredient { ProductId = 14, IngredientId = 22 },
            new ProductIngredient { ProductId = 14, IngredientId = 23 });
    }
}
