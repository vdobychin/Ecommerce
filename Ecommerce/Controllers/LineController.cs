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
    public class LineController : Controller
    {
        private DatabaseContext db;
        private ShopCart shopCart;
        private const int cBraidedLine = 1;
        private const int cMonofilamentLine = 2;

        public LineController(DatabaseContext _db, ShopCart _shopCart)
        {
            db = _db;
            shopCart = _shopCart;
        }

        [HttpGet]
        public ActionResult GetBraidedLines()
        {
            ViewBag.products = db.Products.Where(x => x.SubCatalog.Id == cBraidedLine);
            return View(new ProductShopCartViewModel(shopCart));
        }

        [HttpPost]
        public ActionResult GetBraidedLines(bool Unwinding_30, bool Unwinding_100)
        {
            List<int> UnwindingFilter = new List<int>();
            if (Unwinding_30) UnwindingFilter.Add(30);
            if (Unwinding_100) UnwindingFilter.Add(100);
            ViewBag.products = UnwindingFilter.Any() ? db.Products.Where(i => UnwindingFilter.Contains(i.BraidedLine.Unwinding) && i.SubCatalog.Id == cBraidedLine) : db.Products.Where(x => x.SubCatalog.Id == cBraidedLine);

            return View(new ProductShopCartViewModel(shopCart));
        }

        [HttpGet]
        public ActionResult GetMonofilamentLines()
        {
            ViewBag.products = db.Products.Where(x => x.SubCatalog.Id == cMonofilamentLine);
            return View(new ProductShopCartViewModel(shopCart));
        }

        [HttpPost]
        public ActionResult GetMonofilamentLines(bool Unwinding_30, bool Unwinding_100, bool Unwinding_130, bool Unwinding_150)
        {
            List<int> UnwindingFilter = new List<int>();
            if (Unwinding_30) UnwindingFilter.Add(30);
            if (Unwinding_100) UnwindingFilter.Add(100);
            if (Unwinding_130) UnwindingFilter.Add(130);
            if (Unwinding_150) UnwindingFilter.Add(150);

            ViewBag.products = UnwindingFilter.Any() ? db.Products.Where(i => UnwindingFilter.Contains(i.MonofilamentLine.Unwinding) && i.SubCatalog.Id == cMonofilamentLine) : db.Products.Where(x => x.SubCatalog.Id == cMonofilamentLine);

            return View(new ProductShopCartViewModel(shopCart));
        }
    }
}
