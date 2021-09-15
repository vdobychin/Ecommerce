using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ecommerce.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly ShopCart shopCart;
        private DatabaseContext db;

        public ShopCartController(ShopCart _shopCart, DatabaseContext _db)
        {
            shopCart = _shopCart;
            db = _db;
        }

        public ViewResult Index()
        {
            var items = shopCart.getShopItems();   //Список товаров в корзине
            shopCart.listShopItems = items;

            /*var obj = new ShopCartViewModel
            {
                //shopCart = _shopCart
                listShopItems = items
            };*/
            return View(items);
        }

        
        public IActionResult VerificationAddToCart(int id)
        {
            var item = db.Products.FirstOrDefault(i => i.Id == id);
            if (item != null)
                shopCart.AddToCart(item, 1);
            return PartialView(item);
        }
    }
}
