using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class FeedbackViewModel
    {
        public CallOrder CallOrder { get; set; }
        public Message Message { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
        public IEnumerable<ShopCartItem> ShopCartItems { get; set; }
        public Order Order { get; set; }
        public News News { get; set; }
    }
}
