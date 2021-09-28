using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Ecommerce.ViewModels
{
    public class ReelViewModel
    {
        public ShopCart shopCart { get; set; }
        public int subCatalogId { get; set; }

        public List<SelectListItem> sortName { get; set; } = Sort.sortName;
        public int selectedValue { get; set; }

        public ReelFilter reelFilter { get; set; }

        public ReelViewModel(ShopCart _shopCart, int _subCatalogId = 0, int _selectedValue = 0, ReelFilter _reelFilter = null)
        {
            shopCart = _shopCart;
            subCatalogId = _subCatalogId;
            selectedValue = _selectedValue;
            reelFilter = _reelFilter;
        }
    }
}
