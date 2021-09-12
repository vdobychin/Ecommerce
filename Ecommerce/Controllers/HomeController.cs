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
            Cart cart = new Cart();
            cart.Ammount = 4;
            return View(cart);
        }

        public IActionResult Privacy()
        {
            Cart cart = new Cart();
            cart.Text = "Привет";
            cart.Ammount = 5;
            return View(cart);
            //return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult UpdateShopingCart(Cart cart)
        {
            cart.Ammount = 6;
            return PartialView("_ShowCartPartialView", cart);
        }

    }
}
