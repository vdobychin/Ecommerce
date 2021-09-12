using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<ShopCartItem> ShopCartItem { get; set; }
    }
}
