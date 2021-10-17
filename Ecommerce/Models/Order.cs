using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }        
        public string ShopCartId { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime CreateTime { get; set; }
        public string City { get; set; }

        [Required]
        public string Delivery { get; set; }
        public string AddressDelivery { get; set; }

        [Required]
        public string Payment { get; set; }

        public virtual User User { get; set; }
    }
}
