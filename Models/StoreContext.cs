using demo_crud.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_crud.Context
{
    public class StoreContext : DbContext // NOTE that we extend the DbContext class. DbContext aleady defines how to access data. We need to tell it about our database structure.
    {
    
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        { }
        
        public DbSet<Product> Products { get; set; } // The field that represents the table `products` in our database
    }
}