using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Sort
    {
        public enum OrderBy { priceAsc = 0, priceDesc = 1, quantity = 2, name = 3 };

        public static List<SelectListItem> sortName { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "Цена (возростание)" },
            new SelectListItem { Value = "1", Text = "Цена (убывание)" },
            new SelectListItem { Value = "2", Text = "Наличие" },
            new SelectListItem { Value = "3", Text = "Название" },
        };
    }
}
