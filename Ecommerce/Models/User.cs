using Microsoft.AspNetCore.Mvc;
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

        [Remote(action: "CheckRegister", controller: "Admin", ErrorMessage ="Такой телефон уже используется")]
        [Required(ErrorMessage = "Введите Телефон")]
        [StringLength(15, ErrorMessage = "Не более 15 цифр")]
        public string Phone { get; set; }

        [Remote(action: "CheckRegister", controller: "Admin", ErrorMessage = "Такой Email уже используется")]
        [Required(ErrorMessage = "Введите Email")]
        [EmailAddress(ErrorMessage = "Некоректный электронный адрес")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; }

    }
}
