using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Models.Line;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
            //ViewBag.products = db.Products.ToList();
            ProductShopCartViewModel productShopCartViewModel = new ProductShopCartViewModel(shopCart);
            return View(productShopCartViewModel);
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


            return PartialView("_ShowCart", new ProductShopCartViewModel(shopCart));
        }

        [HttpGet]
        public ActionResult GetMonofilamentLines()
        {
            ViewBag.products = db.Products.ToList();
            
            return View(new ProductShopCartViewModel(shopCart));
        }

        [HttpPost]
        public ActionResult GetMonofilamentLines(bool Unwinding_30, bool Unwinding_100)
        {
            List<int> UnwindingFilter = new List<int>();
            if (Unwinding_30) UnwindingFilter.Add(30);
            if (Unwinding_100) UnwindingFilter.Add(100);
            ViewBag.products = UnwindingFilter.Any() ? db.Products.Where(i => UnwindingFilter.Contains(i.MonofilamentLine.Unwinding)) : db.Products;

            return View(new ProductShopCartViewModel(shopCart));
        }
    }
}
