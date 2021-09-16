using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Models.Line;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private DatabaseContext db;
        private ShopCart shopCart;

        public HomeController(ILogger<HomeController> _logger, DatabaseContext _db, ShopCart _shopCart)
        {
            logger = _logger;
            db = _db;
            shopCart = _shopCart;
        }

        public IActionResult Index()
        {
            ShopCartItem shopCartItem = new ShopCartItem();
            shopCartItem.Quantity = getCountShopCartItems();
            ViewBag.products = db.Products.ToList(); //db.MonofilamentLines.ToList();
            return View(shopCartItem);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult UpdateShopingCart(int id)
        {
            var item = db.Products.FirstOrDefault(i => i.Id == id);
            if (item != null)
                shopCart.AddToCart(item, 1);

            ShopCartItem shopCartItem = new ShopCartItem();
            shopCartItem.Quantity = getCountShopCartItems();
            return PartialView("_ShowCart", shopCartItem);
        }

        /*
        public IActionResult VerificationAddToCart(int id)
        {
            var item = db.Products.FirstOrDefault(i => i.Id == id);
            if (item != null)
                shopCart.AddToCart(item, 1);
            return PartialView(item);
        }
        */
        private int getCountShopCartItems()
        {
            return db.ShopCartItems.Count(a => a.ShopCartId == shopCart.ShopCardId);
        }
    }
}
