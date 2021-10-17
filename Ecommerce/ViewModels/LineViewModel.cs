using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class LineViewModel : FeedbackViewModel
    {
        public DatabaseContext db;
        public int subCatalogId { get; set; }
        public List<SelectListItem> sortName { get; set; } = Constants.sortName;
        public int selectedValue { get; set; }

        //Unwinding
        public bool Unwinding_30 { get; set; }
        public bool Unwinding_100 { get; set; }
        public bool Unwinding_130 { get; set; }
        public bool Unwinding_150 { get; set; }

        //Country
        public bool china { get; set; }
        public bool japan { get; set; }

        //Color
        public bool green { get; set; }
        public bool transparent { get; set; }
        public bool orange { get; set; }
        public bool darkGreen { get; set; }
        public bool lightGreen { get; set; }
        public bool pink { get; set; }

        //price
        public int? priceFrom { get; set; }
        public int? priceTo { get; set; }


        public IEnumerable<Line> getLines()
        {
            IEnumerable<Line> lines;

            List<int> UnwindingList = new List<int>();
            if (Unwinding_30) UnwindingList.Add(30);
            if (Unwinding_100) UnwindingList.Add(100);
            if (Unwinding_130) UnwindingList.Add(130);
            if (Unwinding_150) UnwindingList.Add(150);

            List<string> CountryList = new List<string>();
            if (china) CountryList.Add("Китай");
            if (japan) CountryList.Add("Япония");

            List<string> ColorList = new List<string>();
            if (green) ColorList.Add("зеленый");
            if (transparent) ColorList.Add("прозрачный");
            if (orange) ColorList.Add("оранжевый");
            if (darkGreen) ColorList.Add("темно-зеленый");
            if (lightGreen) ColorList.Add("светло-зеленый");
            if (pink) ColorList.Add("розовый");

            //lines = FilterString.Any() ? db.Lines.Where(i => FilterString.Contains(i.Country)).Include(x => x.Product).ToList() : db.Lines.Include(x => x.Product).ToList();

            if (subCatalogId != 0)
                lines = UnwindingList.Any() ? db.Lines.Where(i => UnwindingList.Contains(i.Unwinding) && i.Product.SubCatalog.Id == subCatalogId).Include(x => x.Product).ToList() : db.Lines.Where(x => x.Product.SubCatalog.Id == subCatalogId).Include(x => x.Product).ToList();
            else
                lines = UnwindingList.Any() ? db.Lines.Where(i => UnwindingList.Contains(i.Unwinding)).Include(x => x.Product).ToList() : db.Lines.Include(x => x.Product).ToList();

            lines = CountryList.Any() ? lines.Where(i => CountryList.Contains(i.Country)).ToList() : lines.ToList();
            lines = ColorList.Any() ? lines.Where(i => ColorList.Contains(i.Color)).ToList() : lines.ToList();

            if (priceFrom > 0)
                lines = lines.Where(x => x.Product.Price >= priceFrom);
            if (priceTo > 0)
                lines = lines.Where(x => x.Product.Price <= priceTo);

            switch (selectedValue)
            {
                case (int)Constants.OrderBy.priceAsc:
                    lines = lines.OrderBy(x => x.Product.Price);
                    break;
                case (int)Constants.OrderBy.priceDesc:
                    lines = lines.OrderByDescending(x => x.Product.Price);
                    break;
                case (int)Constants.OrderBy.quantity:
                    lines = lines.OrderByDescending(x => x.Product.Quantity);
                    break;
                case (int)Constants.OrderBy.name:
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
