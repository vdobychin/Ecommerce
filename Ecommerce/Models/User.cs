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

        /*Для проверки на сервере в DB занят ли телефон, можно атрибутом Remote(action: "VerifyEmail", controller: "Users"), AdditionalFields = nameof(LastName))]
         [Remote(action: "CheckEmail", controller: "Home", ErrorMessage ="Email уже используется")]*/
        [Required(ErrorMessage = "Введите Телефон")]
        [StringLength(15, ErrorMessage = "Не более 15 цифр")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
