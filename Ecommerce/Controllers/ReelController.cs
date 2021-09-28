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

        [HttpGet]
        public IActionResult GetReels(int selectedValue = 0, int subCatalogId = 0)
        {
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();

            ReelViewModel reelViewModel = new()
            {
                db = db,
                selectedValue = selectedValue,
                subCatalogId = subCatalogId
            };
            //ViewBag.reels = GetReelsFilter(reelFilter, subCatalogId: subCatalogId, selectedValue: selectedValue);
            //return View(new ProductViewModel(shopCart, subCatalogId, selectedValue/*, reelFilter*/));

            ViewBag.reels = reelViewModel.getReels();
            return View(reelViewModel);
        }

        [HttpPost]
        public IActionResult GetReels(ReelViewModel reelViewModel)
        {
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();

            //ViewBag.reels = GetReelsFilter(reelFilter, subCatalogId: subCatalogId, selectedValue: selectedValue);
            //return View(new ProductViewModel(shopCart, subCatalogId, selectedValue/*, reelFilter*/));
            //return View();
            reelViewModel.db = db;
            ViewBag.reels = reelViewModel.getReels();
            return View(reelViewModel);
        }
    }
}
