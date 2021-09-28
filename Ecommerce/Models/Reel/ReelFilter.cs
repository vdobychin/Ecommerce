using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class ReelFilter
    {
        //Diameter
        public bool diameter_50 { get; set; }
        public bool diameter_55 { get; set; }
        public bool diameter_60 { get; set; }
        public bool diameter_65 { get; set; }
        public bool diameter_70 { get; set; }
        public bool diameter_75 { get; set; }

        //Material
        public bool plastic { get; set; }
        public bool metalPlastic { get; set; }

        //weight
        public int? weightFrom { get; set; }
        public int? weightTo { get; set; }

        //Company
        public bool peers { get; set; }
        public bool stalker { get; set; }

        //Color
        public bool black { get; set; }
        public bool blackRed  { get; set; }
        public bool green { get; set; }
        public bool blackWhite { get; set; }
        public bool blackOrange { get; set; }
        public bool greenYellow { get; set; }
        public bool lightGreen { get; set; }    //Салатовый
        public bool beige { get; set; }         //Бежевый

        //Feature
        public bool withTrigger  { get; set; }  //С курком

        //price
        public int? priceFrom { get; set; }
        public int? priceTo { get; set; }               
    }
}
