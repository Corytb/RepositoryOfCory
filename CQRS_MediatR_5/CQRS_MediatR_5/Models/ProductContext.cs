using System.Data.Entity;

namespace CQRS_MediatR_5.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
