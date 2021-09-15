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
            ShopCartItem cart = new ShopCartItem();
            cart.Quantity = 4;
            ViewBag.products = db.Products.ToList(); //db.MonofilamentLines.ToList();
            return View(cart);
        }

        public IActionResult Privacy()
        {
            ShopCartItem shopCartItem = new ShopCartItem();
            shopCartItem.Quantity = 5;
            return View(shopCartItem);
            //return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult UpdateShopingCart(ShopCartItem shopCartItem)
        {
            shopCartItem.Quantity = 6;
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
    }
}
