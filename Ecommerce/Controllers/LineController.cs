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
                
        public ActionResult GetLines(LineFilter lineFilter = null, int selectedValue = 0, int subCatalogId = 0)
        {
            ViewBag.products = GetLinesFilter(lineFilter, subCatalogId: subCatalogId, selectedValue: selectedValue);
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

        private IEnumerable<Line> GetLinesFilter(LineFilter lineFilter, int subCatalogId = 0, int selectedValue = 0)
        {
            IEnumerable<Line> lines;

            List<int> UnwindingList = new List<int>();
            if (lineFilter.Unwinding_30) UnwindingList.Add(30);
            if (lineFilter.Unwinding_100) UnwindingList.Add(100);
            if (lineFilter.Unwinding_130) UnwindingList.Add(130);
            if (lineFilter.Unwinding_150) UnwindingList.Add(150);

            List<string> CountryList = new List<string>();
            if (lineFilter.china) CountryList.Add("Китай");
            if (lineFilter.japan) CountryList.Add("Япония");

            List<string> ColorList = new List<string>();
            if (lineFilter.green) ColorList.Add("зеленый");
            if (lineFilter.transparent) ColorList.Add("прозрачный");
            if (lineFilter.orange) ColorList.Add("оранжевый");
            if (lineFilter.darkGreen) ColorList.Add("темно-зеленый");
            if (lineFilter.lightGreen) ColorList.Add("светло-зеленый");
            if (lineFilter.pink) ColorList.Add("розовый");

            //lines = FilterString.Any() ? db.Lines.Where(i => FilterString.Contains(i.Country)).Include(x => x.Product).ToList() : db.Lines.Include(x => x.Product).ToList();

            if (subCatalogId != 0)
                lines = UnwindingList.Any() ? db.Lines.Where(i => UnwindingList.Contains(i.Unwinding) && i.Product.SubCatalog.Id == subCatalogId).Include(x => x.Product).ToList() : db.Lines.Where(x => x.Product.SubCatalog.Id == subCatalogId).Include(x => x.Product).ToList();
            else
                lines = UnwindingList.Any() ? db.Lines.Where(i => UnwindingList.Contains(i.Unwinding)).Include(x => x.Product).ToList() : db.Lines.Include(x => x.Product).ToList();

            lines = CountryList.Any() ? lines.Where(i => CountryList.Contains(i.Country) || ColorList.Contains(i.Color)).ToList() : lines.ToList();

            if (lineFilter.priceFrom > 0)
                lines = lines.Where(x => x.Product.Price >= lineFilter.priceFrom);
            if (lineFilter.priceTo > 0)
                lines = lines.Where(x => x.Product.Price <= lineFilter.priceTo);

            switch (selectedValue)
            {
                case (int)Sort.OrderBy.priceAsc:
                    lines = lines.OrderBy(x => x.Product.Price);
                    break;
                case (int)Sort.OrderBy.priceDesc:
                    lines = lines.OrderByDescending(x => x.Product.Price);
                    break;
                case (int)Sort.OrderBy.quantity:
                    lines = lines.OrderByDescending(x => x.Product.Quantity);
                    break;
                case (int)Sort.OrderBy.name:
                    lines = lines.OrderBy(x => x.Product.Name);
                    break;
                default:
                    lines = lines.OrderBy(x => x.Product.Price);
                    break;
            }

            return lines;
        }
    }
}
