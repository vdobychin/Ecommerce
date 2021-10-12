using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("CallOrder")]
    public class CallOrder
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ваше имя")]
        [Required(ErrorMessage = "Напишите Ваше имя")]
        public string Name { get; set; }

        /*[Remote(action: "CheckEmail", controller: "Home", ErrorMessage ="Email уже используется")]*/
        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "А как мы перезвоним?")]
        public string Phone { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; }
    }
}
