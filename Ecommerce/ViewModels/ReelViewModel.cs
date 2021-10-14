using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.ViewModels
{
    public class ReelViewModel : FeedbackViewModel
    {
        public DatabaseContext db;
        public int subCatalogId { get; set; }
        public List<SelectListItem> sortName { get; set; } = Sort.sortName;
        public int selectedValue { get; set; }

        //Diameter
        public bool diameter_50 { get; set; }
        public bool diameter_55 { get; set; }
        public bool diameter_60 { get; set; }
        public bool diameter_65 { get; set; }
        public bool diameter_70 { get; set; }
        public bool diameter_75 { get; set; }

        //Material
        public bool plastic { get; set; }
        public bool metalPlastic { get; set; }

        //weight
        public int? weightFrom { get; set; }
        public int? weightTo { get; set; }

        //Company
        public bool peers { get; set; }
        public bool stalker { get; set; }

        //Color
        public bool black { get; set; }
        public bool blackRed { get; set; }
        public bool green { get; set; }
        public bool blackWhite { get; set; }
        public bool blackOrange { get; set; }
        public bool greenYellow { get; set; }
        public bool lightGreen { get; set; }    //Салатовый
        public bool beige { get; set; }         //Бежевый

        //Feature
        public bool withTrigger { get; set; }  //С курком

        //price
        public int? priceFrom { get; set; }
        public int? priceTo { get; set; }


        public IEnumerable<Reel> getReels()
        {
            IEnumerable<Reel> reels;

            List<double> diameterList = new List<double>();
            if (diameter_50) diameterList.Add(50);
            if (diameter_55) diameterList.Add(55);
            if (diameter_60) diameterList.Add(60);
            if (diameter_65) diameterList.Add(65);
            if (diameter_70) diameterList.Add(70);
            if (diameter_75) diameterList.Add(75);

            List<string> materialList = new List<string>();
            if (plastic) materialList.Add("пластик");
            if (metalPlastic) materialList.Add("металл/пластик");

            List<string> colorList = new List<string>();
            if (green) colorList.Add("зеленый");
            if (black) colorList.Add("черный");
            if (blackRed) colorList.Add("черно-красный");
            if (blackWhite) colorList.Add("черно-белый");
            if (blackOrange) colorList.Add("черно-оранжевый");
            if (greenYellow) colorList.Add("зелено-желтый");
            if (lightGreen) colorList.Add("салатовый");
            if (beige) colorList.Add("бежевый");

            List<string> companylList = new List<string>();
            if (peers) companylList.Add("Пирс");
            if (stalker) companylList.Add("Сталкер");

            List<string> featureList = new List<string>();
            if (withTrigger) featureList.Add("с курком");

            if (subCatalogId != 0)
                reels = db.Reels.Where(x => x.Product.SubCatalog.Id == subCatalogId).Include(x => x.Product).ToList();
            else
                reels = db.Reels.Include(x => x.Product).ToList();
            /*
            //diameter
            if (subCatalogId != 0)
                reels = diameterList.Any() ? db.Reels.Where(i => diameterList.Contains(i.Diameter) && i.Product.SubCatalog.Id == subCatalogId).Include(x => x.Product).ToList() : db.Reels.Where(x => x.Product.SubCatalog.Id == subCatalogId).Include(x => x.Product).ToList();
            else
                reels = diameterList.Any() ? db.Reels.Where(i => diameterList.Contains(i.Diameter)).Include(x => x.Product).ToList() : db.Reels.Include(x => x.Product).ToList();
            */

            //diameter
            reels = diameterList.Any() ? reels.Where(i => diameterList.Contains(i.Diameter)).ToList() : reels.ToList();

            //material
            reels = materialList.Any() ? reels.Where(i => materialList.Contains(i.Material)).ToList() : reels.ToList();

            //color
            reels = colorList.Any() ? reels.Where(i => colorList.Contains(i.Color)).ToList() : reels.ToList();

            //company
            reels = companylList.Any() ? reels.Where(i => companylList.Contains(i.Company)).ToList() : reels.ToList();

            //price
            if (priceFrom > 0)
                reels = reels.Where(x => x.Product.Price >= priceFrom);
            if (priceTo > 0)
                reels = reels.Where(x => x.Product.Price <= priceTo);

            //weight
            if (weightFrom > 0)
                reels = reels.Where(x => x.Weight >= weightFrom);
            if (weightTo > 0)
                reels = reels.Where(x => x.Weight <= weightTo);

            //feature
            reels = featureList.Any() ? reels.Where(i => featureList.Contains(i.Feature)).ToList() : reels.ToList();

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
