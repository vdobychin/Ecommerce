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
        private const int cBraidedLine = 1;
        private const int cMonofilamentLine = 2;

        public LineController(DatabaseContext _db, ShopCart _shopCart)
        {
            db = _db;
            shopCart = _shopCart;
        }

        [HttpGet]
        public ActionResult GetLines()
        {
            Filter();
            return View(new ProductShopCartViewModel(shopCart));
        }

        [HttpPost]
        public ActionResult GetLines(bool Unwinding_30, bool Unwinding_100, bool Unwinding_130, bool Unwinding_150)
        {
            Filter(Unwinding_30, Unwinding_100, Unwinding_130, Unwinding_150);
            return View(new ProductShopCartViewModel(shopCart));
        }


        [HttpGet]
        public ActionResult GetBraidedLines()
        {
            Filter(cLine: cBraidedLine); //явный параметр
            return View("GetLines", new ProductShopCartViewModel(shopCart, "Плетеный шнур", "GetBraidedLines"));
        }

        [HttpPost]
        public ActionResult GetBraidedLines(bool Unwinding_30, bool Unwinding_100, bool Unwinding_130, bool Unwinding_150)
        {
            Filter(Unwinding_30, Unwinding_100, Unwinding_130, Unwinding_150, cBraidedLine);
            return View("GetLines", new ProductShopCartViewModel(shopCart, "Плетеный шнур", "GetBraidedLines"));
        }

        [HttpGet]
        public ActionResult GetMonofilamentLines()
        {
            Filter(cLine: cMonofilamentLine); //явный параметр
            return View("GetLines", new ProductShopCartViewModel(shopCart, "Монофильная леска", "GetMonofilamentLines"));
        }

        [HttpPost]
        public ActionResult GetMonofilamentLines(bool Unwinding_30, bool Unwinding_100, bool Unwinding_130, bool Unwinding_150)
        {
            Filter(Unwinding_30, Unwinding_100, Unwinding_130, Unwinding_150, cMonofilamentLine);
            return View("GetLines", new ProductShopCartViewModel(shopCart, "Монофильная леска", "GetMonofilamentLines"));
        }

        private void Filter(bool Unwinding_30 = false, bool Unwinding_100 = false, bool Unwinding_130 = false, bool Unwinding_150 = false, int cLine = 0)
        {
            List<int> FilterResult = new List<int>();
            if (Unwinding_30) FilterResult.Add(30);
            if (Unwinding_100) FilterResult.Add(100);
            if (Unwinding_130) FilterResult.Add(130);
            if (Unwinding_150) FilterResult.Add(150);

            if(cLine != 0)
                ViewBag.products = FilterResult.Any() ? db.Lines.Where(i => FilterResult.Contains(i.Unwinding) && i.Product.SubCatalog.Id == cLine).Include(x => x.Product).ToList() : db.Lines.Where(x => x.Product.SubCatalog.Id == cLine).Include(x => x.Product).ToList();
            else
                ViewBag.products = FilterResult.Any() ? db.Lines.Where(i => FilterResult.Contains(i.Unwinding)).Include(x => x.Product).ToList() : db.Lines.Include(x => x.Product).ToList();
        }
    }
}
