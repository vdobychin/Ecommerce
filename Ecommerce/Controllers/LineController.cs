using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class LineController : Controller
    {
        private DatabaseContext db;
        private ShopCart shopCart;

        public LineController(DatabaseContext _db, ShopCart _shopCart)
        {
            db = _db;
            shopCart = _shopCart;
        }

        [HttpGet]
        public ActionResult GetLines(int selectedValue = 0, int subCatalogId = 0)
        {
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();

            LineViewModel lineViewModel = new()
            {
                db = db,
                selectedValue = selectedValue,
                subCatalogId = subCatalogId
            };

            ViewBag.lines = lineViewModel.getLines();
            return View(lineViewModel);
        }

        [HttpPost]
        public ActionResult GetLines(LineViewModel lineViewModel)
        {
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();

            lineViewModel.db = db;
            ViewBag.lines = lineViewModel.getLines();
            return View(lineViewModel);
        }       
    }
}
