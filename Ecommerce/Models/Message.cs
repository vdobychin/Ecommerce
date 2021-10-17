using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("Message")]
    public class Message //: IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        //[Remote(action: "CheckMessage", controller: "Home", AdditionalFields = nameof(Email))]
        [Display(Name = "Ваше имя")]
        [Required(ErrorMessage = "Напишите Ваше имя")]
        public string Name { get; set; }


        //[Remote(action: "CheckMessage", controller: "Home", AdditionalFields = nameof(Phone))]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Некоректная электронная почта")]
        [Required(ErrorMessage = "Электронная почта")]
        public string Email { get; set; }
        

        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; }


        [Required(ErrorMessage = "Отсутствует текст сообщения")]
        public string Text { get; set; }

        /*
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Phone == null && Email == null)
            {
                //List<ValidationResult> errors = new List<ValidationResult>();
                yield return new ValidationResult("Classic movies must have a release year no later than ");
            }
        }
        */
    }
}
