using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
            //ProductViewModel productViewModel = new ProductViewModel() { shopCart = shopCart };
            //return View(productViewModel);
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult UpdateShopingCart(int id)
        {
            var item = db.Products.FirstOrDefault(i => i.ProductId == id);
            if (item != null)
                shopCart.AddToCart(item, 1);
            
            //return PartialView("~/Views/Shared/_ShowCart.cshtml", new ProductViewModel() { shopCart = shopCart});
            
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();
            return PartialView("~/Views/Shared/_ShowCart.cshtml");
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult RequestCall()
        {
            return PartialView("RequestCall");
        }
        [HttpPost]
        public IActionResult RequestCall(CallOrder callOrder, string ReturnUrl)
        {
            callOrder.CreateTime = DateTime.Now;
            db.CallOrders.Add(callOrder);
            db.SaveChanges();

            if (String.IsNullOrEmpty(ReturnUrl))
                return Redirect("/Home/Index");
            else
                return Redirect(ReturnUrl);
        }

        public IActionResult RequestMail()
        {
            return PartialView("RequestMail");
        }
        [HttpPost]
        public IActionResult RequestMail(Message message, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError("", "Имя и пароль не должны совпадать");
                return PartialView();
                //return NoContent();
                //return BadRequest(ModelState);
            }

            if (String.IsNullOrEmpty(message.Email) || String.IsNullOrEmpty(message.Phone))
                return NoContent();

            if (!TryValidateModel(message))
            {
                return Json(false);
            }

            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckMessage(string Email, string Phone)
        {
            if (String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Phone))
                return NoContent();

            return Json(false);
        }


        [HttpPost]
        public IActionResult RequestCall2(CallOrder callOrder, string ReturnUrl)
        {
            callOrder.CreateTime = DateTime.Now;
            db.CallOrders.Add(callOrder);
            db.SaveChanges();

            if (String.IsNullOrEmpty(ReturnUrl))
                return Redirect("/Home/Index");
            else
                return Redirect(ReturnUrl);
        }
    }
}
