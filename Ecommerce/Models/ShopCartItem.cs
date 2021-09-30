using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("ShopCartItem")]
    public class ShopCartItem
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }

        [DisplayName("Количество")]
        public int Quantity { get; set; }
        public string ShopCartId { get; set; }

        public Product Product { get; set; }
    }
}
