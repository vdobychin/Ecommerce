using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("Login")]
    public class Login
    {
        [Key, Required]
        public string UserName { get; set; }        
        public string Salt { get; set; }
        public string Hash { get; set; }
        public string Role { get; set; }
        public virtual User User { get; set; }
    }
}
