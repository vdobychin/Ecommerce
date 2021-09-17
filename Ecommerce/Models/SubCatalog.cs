using Ecommerce.Models.Line;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("SubCatalog")]
    public class SubCatalog
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatalogId { get; set; }
        public virtual Catalog Catalog { get; set; }

        //public List<MonofilamentLine> MonofilamentLines { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<MonofilamentLine> MonofilamentLines { get; set; }
    }
}
