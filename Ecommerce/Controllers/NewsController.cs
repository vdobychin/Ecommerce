using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class NewsController : Controller
    {
        private readonly ShopCart shopCart;
        private DatabaseContext db;
        public NewsController(ShopCart _shopCart, DatabaseContext _db)
        {
            shopCart = _shopCart;
            db = _db;
        }

        public IActionResult Index(int id)
        {
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();

            FeedbackViewModel feedbackViewModel = new();
            feedbackViewModel.News = db.News.First(i => i.Id == id);
            return View(feedbackViewModel);
        }
    }
}
