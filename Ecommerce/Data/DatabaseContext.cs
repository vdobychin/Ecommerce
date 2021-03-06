using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public virtual DbSet<ShopCartItem> ShopCartItems { get; set; }
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<SubCatalog> SubCatalogs { get; set; }
        public virtual DbSet<Line> Lines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Reel> Reels { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<CallOrder> CallOrders { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
    }
}
