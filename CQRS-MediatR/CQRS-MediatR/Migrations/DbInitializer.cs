using CQRS_MediatR.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS_MediatR.Migrations
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Customer>().HasData(
                   new Customer() { Id = 1, BusinessName = "Great Company 1", PhoneNumber = "612-867-5309" },
                   new Customer() { Id = 2, BusinessName = "Wonderful Wonder-Bread", PhoneNumber = "612-407-9242" },
                   new Customer() { Id = 3, BusinessName = "Wow!", PhoneNumber = "612-866-6537" }
            );
            modelBuilder.Entity<Order>().HasData(
                   new Order() { Id = 1, CustomerId = 1, ProductId = 1, OrderDate = DateTime.Parse("12-15-2021") },
                   new Order() { Id = 2, CustomerId = 1, ProductId = 2, OrderDate = DateTime.Parse("12-15-2021") },
                   new Order() { Id = 3, CustomerId = 2, ProductId = 1, OrderDate = DateTime.Parse("12-15-2021") },
                   new Order() { Id = 4, CustomerId = 2, ProductId = 3, OrderDate = DateTime.Parse("12-15-2021") },
                   new Order() { Id = 5, CustomerId = 3, ProductId = 1, OrderDate = DateTime.Parse("12-15-2021") }
            );
            modelBuilder.Entity<Product>().HasData(
                   new Product() { Id = 1, Name = "Shirt", Price = 49.99M },
                   new Product() { Id = 2, Name = "Pants", Price = 29.99M },
                   new Product() { Id = 3, Name = "Socks", Price = 39.99M },
                   new Product() { Id = 4, Name = "Shoes", Price = 19.99M }
            );
        }
    }
}
