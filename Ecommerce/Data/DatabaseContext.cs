using Ecommerce.Models;
using Ecommerce.Models.Line;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public virtual DbSet<ShopCartItem> ShopCartItems { get; set; }
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<SubCatalog> SubCatalogs { get; set; }
        public virtual DbSet<MonofilamentLine> MonofilamentLines { get; set; }
        public virtual DbSet<BraidedLine> BraidedLines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
