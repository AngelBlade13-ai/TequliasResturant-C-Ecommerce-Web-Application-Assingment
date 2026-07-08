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
            new Category { CategoryId = 1, Name = "Appetizer" },
            new Category { CategoryId = 2, Name = "Entree" },
            new Category { CategoryId = 3, Name = "Side Dish" },
            new Category { CategoryId = 4, Name = "Dessert" },
            new Category { CategoryId = 5, Name = "Beverage" });

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, Name = "Beef Taco", Description = "A delicious beef taco.", Price = 2.50m, Stock = 100, CategoryId = 2 },
            new Product { ProductId = 2, Name = "Chicken Taco", Description = "A delicious chicken taco.", Price = 1.99m, Stock = 101, CategoryId = 2 },
            new Product { ProductId = 3, Name = "Fish Taco", Description = "A delicious fish taco.", Price = 3.99m, Stock = 90, CategoryId = 2 });

        modelBuilder.Entity<Ingredient>().HasData(
            //add mexican restaurant ingredients here
            new Ingredient { IngredientId = 1, Name = "Beef" },
            new Ingredient { IngredientId = 2, Name = "Chicken" },
            new Ingredient { IngredientId = 3, Name = "Fish" },
            new Ingredient { IngredientId = 4, Name = "Tortilla" },
            new Ingredient { IngredientId = 5, Name = "Lettuce" },
            new Ingredient { IngredientId = 6, Name = "Tomato" });

        modelBuilder.Entity<ProductIngredient>().HasData(
            new ProductIngredient { ProductId = 1, IngredientId = 1 },
            new ProductIngredient { ProductId = 1, IngredientId = 4 },
            new ProductIngredient { ProductId = 1, IngredientId = 5 },
            new ProductIngredient { ProductId = 1, IngredientId = 6 },
            new ProductIngredient { ProductId = 2, IngredientId = 2 },
            new ProductIngredient { ProductId = 2, IngredientId = 4 },
            new ProductIngredient { ProductId = 2, IngredientId = 5 },
            new ProductIngredient { ProductId = 2, IngredientId = 6 },
            new ProductIngredient { ProductId = 3, IngredientId = 3 },
            new ProductIngredient { ProductId = 3, IngredientId = 4 },
            new ProductIngredient { ProductId = 3, IngredientId = 5 },
            new ProductIngredient { ProductId = 3, IngredientId = 6 });
    }
}