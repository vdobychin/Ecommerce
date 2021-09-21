using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("Line")]
    public class Line
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }
        public double Diameter { get; set; }                //Диаметр
        public int Unwinding { get; set; }                  //Размотка
        public double BreakingLoad_kg { get; set; }         //Разрывная нагрузка в кг
        
        //[Required]
        public double? BreakingLoad_lb { get; set; }        //Разрывная нагрузка в lb

        public string Country { get; set; }                 //Страна производитель
        public string Color { get; set; }                   //Цвет
        public string Company { get; set; }                 //Компания

        //public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
