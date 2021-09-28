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
    public class ReelController : Controller
    {
        private DatabaseContext db;
        private ShopCart shopCart;

        public ReelController(DatabaseContext _db, ShopCart _shopCart)
        {
            db = _db;
            shopCart = _shopCart;
        }

        public IActionResult Index(ReelFilter reelFilter = null, int selectedValue = 0, int subCatalogId = 0)
        {
            ViewBag.reels = GetReelsFilter(reelFilter, subCatalogId: subCatalogId, selectedValue: selectedValue);
            //return View(new ProductViewModel(shopCart, subCatalogId, selectedValue/*, reelFilter*/));
            return View();
        }

        private IEnumerable<Reel> GetReelsFilter(ReelFilter reelFilter, int subCatalogId = 0, int selectedValue = 0)
        {
            IEnumerable<Reel> reels;

            List<double> diameterList = new List<double>();
            if (reelFilter.diameter_50) diameterList.Add(50);
            if (reelFilter.diameter_55) diameterList.Add(55);
            if (reelFilter.diameter_60) diameterList.Add(60);
            if (reelFilter.diameter_65) diameterList.Add(65);
            if (reelFilter.diameter_70) diameterList.Add(70);
            if (reelFilter.diameter_75) diameterList.Add(75);

            List<string> materialList = new List<string>();
            if (reelFilter.plastic) materialList.Add("пластик");
            if (reelFilter.metalPlastic) materialList.Add("металл/пластик");

            List<string> colorList = new List<string>();
            if (reelFilter.green) colorList.Add("зеленый");
            if (reelFilter.black) colorList.Add("черный");
            if (reelFilter.blackRed) colorList.Add("черно-красный");
            if (reelFilter.blackWhite) colorList.Add("черно-белый");
            if (reelFilter.blackOrange) colorList.Add("черно-оранжевый");
            if (reelFilter.greenYellow) colorList.Add("зелено-желтый");
            if (reelFilter.lightGreen) colorList.Add("салатовый");
            if (reelFilter.beige) colorList.Add("бежевый");

            List<string> companylList = new List<string>();
            if (reelFilter.peers) companylList.Add("Пирс");
            if (reelFilter.stalker) companylList.Add("Сталкер");

            List<string> featureList = new List<string>();
            if (reelFilter.peers) featureList.Add("с курком");

            //diameter
            if (subCatalogId != 0)
                reels = diameterList.Any() ? db.Reels.Where(i => diameterList.Contains(i.Diameter) && i.Product.SubCatalog.Id == subCatalogId).Include(x => x.Product).ToList() : db.Reels.Where(x => x.Product.SubCatalog.Id == subCatalogId).Include(x => x.Product).ToList();
            else
                reels = diameterList.Any() ? db.Reels.Where(i => diameterList.Contains(i.Diameter)).Include(x => x.Product).ToList() : db.Reels.Include(x => x.Product).ToList();

            //material
            reels = materialList.Any() ? reels.Where(i => materialList.Contains(i.Color) || materialList.Contains(i.Color)).ToList() : reels.ToList();

            //color
            reels = colorList.Any() ? reels.Where(i => colorList.Contains(i.Color) || colorList.Contains(i.Color)).ToList() : reels.ToList();

            //company
            reels = companylList.Any() ? reels.Where(i => companylList.Contains(i.Color) || companylList.Contains(i.Color)).ToList() : reels.ToList();

            //price
            if (reelFilter.priceFrom > 0)
                reels = reels.Where(x => x.Product.Price >= reelFilter.priceFrom);
            if (reelFilter.priceTo > 0)
                reels = reels.Where(x => x.Product.Price <= reelFilter.priceTo);

            //weight
            if (reelFilter.weightFrom > 0)
                reels = reels.Where(x => x.Weight >= reelFilter.weightFrom);
            if (reelFilter.weightTo > 0)
                reels = reels.Where(x => x.Weight <= reelFilter.weightTo);

            //feature
            reels = featureList.Any() ? reels.Where(i => featureList.Contains(i.Color) || featureList.Contains(i.Color)).ToList() : reels.ToList();

            //sort
            switch (selectedValue)
            {
                case (int)Sort.OrderBy.priceAsc:
                    reels = reels.OrderBy(x => x.Product.Price);
                    break;
                case (int)Sort.OrderBy.priceDesc:
                    reels = reels.OrderByDescending(x => x.Product.Price);
                    break;
                case (int)Sort.OrderBy.quantity:
                    reels = reels.OrderByDescending(x => x.Product.Quantity);
                    break;
                case (int)Sort.OrderBy.name:
                    reels = reels.OrderBy(x => x.Product.Name);
                    break;
                default:
                    reels = reels.OrderBy(x => x.Product.Price);
                    break;
            }

            return reels;
        }
    }
}
