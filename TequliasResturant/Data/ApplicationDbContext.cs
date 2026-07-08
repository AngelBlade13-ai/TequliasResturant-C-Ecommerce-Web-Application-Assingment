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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProductIngredient>()
            .HasKey(productIngredient => new { productIngredient.ProductId, productIngredient.IngredientId });

        builder.Entity<ProductIngredient>()
            .HasOne(productIngredient => productIngredient.Product)
            .WithMany(product => product.ProductIngredients)
            .HasForeignKey(productIngredient => productIngredient.ProductId);

        builder.Entity<ProductIngredient>()
            .HasOne(productIngredient => productIngredient.Ingredient)
            .WithMany(ingredient => ingredient.ProductIngredients)
            .HasForeignKey(productIngredient => productIngredient.IngredientId);

        builder.Entity<Product>()
            .Property(product => product.Price)
            .HasPrecision(18, 2);

        builder.Entity<OrderItem>()
            .Property(orderItem => orderItem.Price)
            .HasPrecision(18, 2);

        builder.Entity<Order>()
            .Property(order => order.TotalAmount)
            .HasPrecision(18, 2);

        builder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Tacos", Description = "Starter seed data for follow-along testing." },
            new Category { CategoryId = 2, Name = "Drinks", Description = "Add more categories during the tutorial." });

        builder.Entity<Product>().HasData(
            new Product { ProductId = 1, Name = "Beef Taco", Description = "Sample menu item.", Price = 4.99m, Stock = 25, CategoryId = 1 },
            new Product { ProductId = 2, Name = "Veggie Taco", Description = "Sample menu item.", Price = 5.49m, Stock = 20, CategoryId = 1 },
            new Product { ProductId = 3, Name = "Horchata", Description = "Sample drink.", Price = 2.99m, Stock = 30, CategoryId = 2 });

        builder.Entity<Ingredient>().HasData(
            new Ingredient { IngredientId = 1, Name = "Tortilla" },
            new Ingredient { IngredientId = 2, Name = "Beef" },
            new Ingredient { IngredientId = 3, Name = "Lettuce" });

        builder.Entity<ProductIngredient>().HasData(
            new ProductIngredient { ProductId = 1, IngredientId = 1 },
            new ProductIngredient { ProductId = 1, IngredientId = 2 },
            new ProductIngredient { ProductId = 2, IngredientId = 1 },
            new ProductIngredient { ProductId = 2, IngredientId = 3 });
    }
}
