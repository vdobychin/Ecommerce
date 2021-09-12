using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("ShopCartItem")]
    public class ShopCartItem
    {
        [Key]
        public int Id { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string ShopCartId { get; set; }

        //[Key]
        public int ProductId { get; set; }
        //public Product product { get; set; }
    }
}
