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
            ProductViewModel productViewModel = new()
            {
                db = db,
                selectedValue = selectedValue,
                subCatalogId = subCatalogId
            };

            ViewBag.lines = productViewModel.getLines();
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult GetLines(ProductViewModel productViewModel)
        {
            productViewModel.db = db;
            ViewBag.lines = productViewModel.getLines();
            return View(productViewModel);
        }       
    }
}
