using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class LineFilter
    {
        //Unwinding
        public bool Unwinding_30 { get; set; }
        public bool Unwinding_100 { get; set; }
        public bool Unwinding_130 { get; set; }
        public bool Unwinding_150 { get; set; }

        //Country
        public bool china { get; set; }
        public bool japan { get; set; }

        //Color
        public bool green { get; set; }
        public bool transparent { get; set; }
        public bool orange { get; set; }
        public bool darkGreen { get; set; }
        public bool lightGreen { get; set; }
        public bool pink { get; set; }

        //price
        public int priceFrom { get; set; }
        public int priceTo { get; set; }
    }
}
