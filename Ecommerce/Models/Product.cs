using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }      //Ключ
        public string Name { get; set; }        //Имя
        public string Img { get; set; }         //Url картнки
        public decimal Price { get; set; }      //Цена
        public bool isFavourite { get; set; }   //Для отображения при старте
        public int? Quantity { get; set; }      //Количество (В наличии)

        //[ForeignKey("CatalogId")]
        //public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }

        //[ForeignKey("SubCatalogId")]
        //public int SubCatalogId { get; set; }
        public SubCatalog SubCatalog { get; set; }

        public ICollection <ShopCartItem> ShopCartItems { get; set; }


        //public int Id { get; set; }
        public Line Line { get; set; }
    }
}
