using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("Reel")]
    public class Reel
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }
        public double Diameter { get; set; }                //Диаметр
        public string Material { get; set; }                //Материал
        public double Weight { get; set; }                  //Вес, г
        public string Color { get; set; }                   //Цвет
        public string Feature { get; set; }                 //Особенность
        public string Company { get; set; }                 //Компания

        //public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
