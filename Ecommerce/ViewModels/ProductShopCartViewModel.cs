using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class ProductShopCartViewModel
    {
        public ShopCart shopCart { get; set; }
        public int subCatalogId { get; set; }
        public string actionName { get; set; }
        public ProductShopCartViewModel(ShopCart _shopCart, int _subCatalogId = 0)
        {
            shopCart = _shopCart;
            subCatalogId = _subCatalogId;
        }

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
        public bool transparent { get; set; } //прозрачный
        public bool orange { get; set; }
        public bool darkGreen { get; set; }
        public bool lightGreen { get; set; }
        public bool pink { get; set; }



        public Line Line { get; set; }

        public Product Product { get; set; }
    }
}
