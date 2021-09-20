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
        public ProductShopCartViewModel(ShopCart _shopCart)
        {
            shopCart = _shopCart;
        }

        public bool Unwinding_30 { get; set; }
        public bool Unwinding_100 { get; set; }
        public Product Product { get; set; }
    }
}
