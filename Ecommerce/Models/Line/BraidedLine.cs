using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Line
{
    [Table("BraidedLine")]
    public class BraidedLine
    {
        [Key]
        //[ForeignKey(Product.Id)]
        public int ProductId { get; set; }
        public string Company { get; set; }
        public double Diameter { get; set; }
        public int Unwinding { get; set; }
        public double BreakingLoad { get; set; }            //Разрывная нагрузка
        public string CountryManufacturing { get; set; }
        public string Color { get; set; }

        //public virtual Catalog Catalog { get; set; }
        //public virtual SubCatalog SubCatalog { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
        
        public int Id { get; set; }
        public virtual Product Product { get; set; }
    }
}
