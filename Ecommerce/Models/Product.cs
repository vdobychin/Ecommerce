using Ecommerce.Models.Line;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }          // Первичный ключ
        public string Name { get; set; }       // Короткое описание
        public string Mark { get; set; }            // марка
        public string Img { get; set; }             // Url картнки
        public decimal Price { get; set; }              // Цена
        public bool isFavourite { get; set; }       // Для отображения при старте

        public virtual Catalog Catalog { get; set; }
        public virtual SubCatalog SubCatalog { get; set; }
        public virtual ICollection <ShopCartItem> ShopCartItems { get; set; }
        //public virtual ICollection<MonofilamentLine> MonofilamentLines { get; set; }
        public virtual MonofilamentLine MonofilamentLine { get; set; }
    }
}
