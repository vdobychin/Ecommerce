using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("Credential")]
    public class Credential
    {
        [Key]
        public int Id { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
