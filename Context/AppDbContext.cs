//The name of this file is AppDbContext.cs is set by default in the project template.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MyStore.Entities;
using MyStore.Entitiess;

namespace MyStore.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // This is a constructor. First method that runs when the class is instantiated.
        //base means that the options parameter is passed to the base class DbContext
        {

        }
        //Table configuration
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        //Entities that act as tables in the database.
        //We have followed the correct naming conventions for DbSet properties in order to migrate the database to sql server.

        protected override void OnModelCreating(ModelBuilder modelBuilder) //method to configure the model
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configurations can be added here if needed. 

            modelBuilder.Entity<Category>(e => // this entity represents the Category table
            {
                e.HasKey("CategoryId");
                e.Property("CategoryId").ValueGeneratedOnAdd(); // Auto-increment primary key
                e.HasData(
                    new Category { CategoryId = 1, Name = "Technology" },// Seed data, this means that these records will be added to the database when it is created.
                    new Category { CategoryId = 2, Name = "Bedroom" }
                );
            });

            modelBuilder.Entity<User>(e => // this entity represents the Category table
            {
                e.HasKey("UserId");
                e.Property("UserId").ValueGeneratedOnAdd(); // Auto-increment primary key
            });

            modelBuilder.Entity<Product>(e => // this entity represents the Category table
            {
                e.HasKey("ProductId");
                e.Property("ProductId").ValueGeneratedOnAdd(); // Auto-increment primary key
                e.Property("Price").HasColumnType("decimal(10,2)"); // Set the precision and scale for the Price column
                e.HasOne(e => e.Category)
                 .WithMany(p => p.Products)
                 .HasForeignKey(e => e.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict); 
            });
            
            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey("OrderId");
                e.Property("OrderId").ValueGeneratedOnAdd();
                e.Property("TotalAmount").HasColumnType("decimal(10,2)");
                e.HasOne(e => e.User)
                 .WithMany(p => p.Orders)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<OrderItem>(e =>
            {
                e.HasKey("OrderItemId");
                e.Property("OrderItemId").ValueGeneratedOnAdd();
                e.Property("Price").HasColumnType("decimal(10,2)");
                e.HasOne(e => e.Order)
                 .WithMany(p => p.OrderItems)
                 .HasForeignKey(e => e.OrderId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(e => e.Product)
                 .WithMany()
                 .HasForeignKey(e => e.ProductId)
                 .OnDelete(DeleteBehavior.Restrict); 
            });

                
        }

    }
}