using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class LineViewModel
    {
        public ShopCart shopCart { get; set; }
        public int subCatalogId { get; set; }

        public List<SelectListItem> sortName { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "Цена (возростание)" },
            new SelectListItem { Value = "1", Text = "Цена (убывание)" },
            new SelectListItem { Value = "2", Text = "Наличие" },
            new SelectListItem { Value = "3", Text = "Название" },
        };
        public int selectedValue { get; set; }

        public LineFilter lineFilter { get; set; }

        public LineViewModel(ShopCart _shopCart, int _subCatalogId = 0, int _selectedValue = 0, LineFilter _lineFilter = null)
        {
            shopCart = _shopCart;
            subCatalogId = _subCatalogId;
            selectedValue = _selectedValue;
            lineFilter = _lineFilter;
        }

        
    }
}
