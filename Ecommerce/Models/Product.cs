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

        //[ForeignKey("CatalogId")]
        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }

        //[ForeignKey("SubCatalogId")]
        public int SubCatalogId { get; set; }
        public SubCatalog SubCatalog { get; set; }
    }
}
