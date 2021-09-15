using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Line
{
    [Table("MonofilamentLine")]
    public class MonofilamentLine
    {
        [Key]
        [ForeignKey("Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public double Diameter { get; set; }
        public int Unwinding { get; set; }
        public double BreakingLoad { get; set; }            //Разрывная нагрузка
        public string CountryManufacturing { get; set; }
        public string Color { get; set; }

        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }

        public int SubCatalogId { get; set; }
        public SubCatalog SubCatalog { get; set; }
    }
}
