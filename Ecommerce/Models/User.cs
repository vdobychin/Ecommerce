using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Имя")]
        public string Name { get; set; }

        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите Фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }
    }
}
