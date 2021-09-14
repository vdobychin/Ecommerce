using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("Catalog")]
    public class Catalog
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCatalog> SubCatalogs { get; set; }
    }
}
