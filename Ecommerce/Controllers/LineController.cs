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
        public ActionResult GetLines(int subCatalogId = 0, int sort = 0)
        {
            Filter(cLine: subCatalogId, sort: sort);  //явный параметр
            return View(new ProductShopCartViewModel(shopCart, subCatalogId));
        }

        [HttpPost]
        public ActionResult GetLines(bool Unwinding_30, bool Unwinding_100, bool Unwinding_130, bool Unwinding_150, bool china, bool japan,
            bool green, bool transparent, bool orange, bool darkGreen, bool lightGreen, bool pink, int subCatalogId = 0, int sort = 0)
        {
            Filter(Unwinding_30, Unwinding_100, Unwinding_130, Unwinding_150, china, japan, green, transparent, orange, darkGreen, lightGreen, pink, cLine: subCatalogId, sort: sort);
            return View(new ProductShopCartViewModel(shopCart, subCatalogId));
        }


        /*
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
        */

        private void Filter(bool Unwinding_30 = false, bool Unwinding_100 = false, bool Unwinding_130 = false, bool Unwinding_150 = false, bool china = false, bool japan = false,
            bool green = false, bool transparent = false, bool orange = false, bool darkGreen = false, bool lightGreen = false, bool pink = false, int cLine = 0, int sort = 0)
        {
            IEnumerable<Line> lines;

            List<int> UnwindingFilter = new List<int>();
            if (Unwinding_30) UnwindingFilter.Add(30);
            if (Unwinding_100) UnwindingFilter.Add(100);
            if (Unwinding_130) UnwindingFilter.Add(130);
            if (Unwinding_150) UnwindingFilter.Add(150);

            List<string> CountryFilter = new List<string>();
            if (china) CountryFilter.Add("Китай");
            if (japan) CountryFilter.Add("Япония");

            List<string> ColorFilter = new List<string>();
            if (green) CountryFilter.Add("зеленый");
            if (transparent) CountryFilter.Add("прозрачный");
            if (orange) CountryFilter.Add("оранжевый");
            if (darkGreen) CountryFilter.Add("темно-зеленый");
            if (lightGreen) CountryFilter.Add("светло-зеленый");
            if (pink) CountryFilter.Add("розовый");

            //lines = FilterString.Any() ? db.Lines.Where(i => FilterString.Contains(i.Country)).Include(x => x.Product).ToList() : db.Lines.Include(x => x.Product).ToList();

            if (cLine != 0)
                lines = UnwindingFilter.Any() ? db.Lines.Where(i => UnwindingFilter.Contains(i.Unwinding) && i.Product.SubCatalog.Id == cLine).Include(x => x.Product).ToList() : db.Lines.Where(x => x.Product.SubCatalog.Id == cLine).Include(x => x.Product).ToList();
            else
                lines = UnwindingFilter.Any() ? db.Lines.Where(i => UnwindingFilter.Contains(i.Unwinding)).Include(x => x.Product).ToList() : db.Lines.Include(x => x.Product).ToList();

            lines = CountryFilter.Any() ? lines.Where(i => CountryFilter.Contains(i.Country) || ColorFilter.Contains(i.Color)).ToList() : lines.ToList();

            switch (sort)
            {
                case (int)Sort.OrderBy.priceAsc:
                    lines = lines.OrderBy(x => x.Product.Price);
                    break;
                case (int)Sort.OrderBy.priceDesc:
                    lines = lines.OrderByDescending(x => x.Product.Price);
                    break;
                default:
                    lines = lines.OrderBy(x => x.Product.Price);
                    break;
            }

            ViewBag.products = lines;
        }
    }
}
