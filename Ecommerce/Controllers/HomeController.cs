using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ShopCartItem cart = new ShopCartItem();
            cart.Quantity = 4;
            return View(cart);
        }

        public IActionResult Privacy()
        {
            ShopCartItem shopCartItem = new ShopCartItem();
            //shopCartItem.Text = "Привет";
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
            return PartialView("_ShowCartPartialView", shopCartItem);
        }

    }
}
