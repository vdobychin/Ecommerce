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

        
        public ActionResult GetLines(LineFilter lineFilter = null, int subCatalogId = 0, int selectedValue = 0)
        {
            ViewBag.products = GetLinesFilter(lineFilter, cLine: subCatalogId, selectedValue: selectedValue);
            return View(new LineViewModel(shopCart, subCatalogId, selectedValue));
        }

        /*
        [HttpPost]
        public ActionResult GetLines(bool Unwinding_30, bool Unwinding_100, bool Unwinding_130, bool Unwinding_150, bool china, bool japan,
            bool green, bool transparent, bool orange, bool darkGreen, bool lightGreen, bool pink, LineFilter lineFilter = null, int subCatalogId = 0, int selectedValue = 0)
        {
            ViewBag.products = GetLinesFilter(lineFilter, cLine: subCatalogId, selectedValue: selectedValue);
            return View(new LineViewModel(shopCart, subCatalogId, selectedValue, lineFilter));
        }*/

        private IEnumerable<Line> GetLinesFilter(LineFilter lineFilter, int cLine = 0, int selectedValue = 0)
        {
            IEnumerable<Line> lines;

            List<int> UnwindingFilter = new List<int>();
            if (lineFilter.Unwinding_30) UnwindingFilter.Add(30);
            if (lineFilter.Unwinding_100) UnwindingFilter.Add(100);
            if (lineFilter.Unwinding_130) UnwindingFilter.Add(130);
            if (lineFilter.Unwinding_150) UnwindingFilter.Add(150);

            List<string> CountryFilter = new List<string>();
            if (lineFilter.china) CountryFilter.Add("Китай");
            if (lineFilter.japan) CountryFilter.Add("Япония");

            List<string> ColorFilter = new List<string>();
            if (lineFilter.green) CountryFilter.Add("зеленый");
            if (lineFilter.transparent) CountryFilter.Add("прозрачный");
            if (lineFilter.orange) CountryFilter.Add("оранжевый");
            if (lineFilter.darkGreen) CountryFilter.Add("темно-зеленый");
            if (lineFilter.lightGreen) CountryFilter.Add("светло-зеленый");
            if (lineFilter.pink) CountryFilter.Add("розовый");

            //lines = FilterString.Any() ? db.Lines.Where(i => FilterString.Contains(i.Country)).Include(x => x.Product).ToList() : db.Lines.Include(x => x.Product).ToList();

            if (cLine != 0)
                lines = UnwindingFilter.Any() ? db.Lines.Where(i => UnwindingFilter.Contains(i.Unwinding) && i.Product.SubCatalog.Id == cLine).Include(x => x.Product).ToList() : db.Lines.Where(x => x.Product.SubCatalog.Id == cLine).Include(x => x.Product).ToList();
            else
                lines = UnwindingFilter.Any() ? db.Lines.Where(i => UnwindingFilter.Contains(i.Unwinding)).Include(x => x.Product).ToList() : db.Lines.Include(x => x.Product).ToList();

            lines = CountryFilter.Any() ? lines.Where(i => CountryFilter.Contains(i.Country) || ColorFilter.Contains(i.Color)).ToList() : lines.ToList();

            switch (selectedValue)
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

            return lines;
        }
    }
}
