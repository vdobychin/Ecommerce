using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ecommerce.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly ShopCart shopCart;
        private DatabaseContext db;

        public ShopCartController(ShopCart _shopCart, DatabaseContext _db)
        {
            shopCart = _shopCart;
            db = _db;
        }

        public ViewResult Index()
        {
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();

            //var items = shopCart.getShopItems();   //Список товаров в корзине
            shopCart.listShopItems = shopCart.getShopItems();

            /*var obj = new ShopCartViewModel
            {
                //shopCart = _shopCart
                listShopItems = items
            };*/
            return View(shopCart.listShopItems);
        }

        //Удаление записи  из корзины        
        public RedirectToActionResult DeleteCard(int shopCartItemId)
        {
            shopCart.DeleteToCard(shopCartItemId);
            return RedirectToAction("Index");
        }


        public ActionResult VerificationDeleteToCart(int ProductId)
        {
            ShopCartItem shopCartItem = db.ShopCartItems.Where(g => g.Product.ProductId == ProductId && g.ShopCartId == shopCart.ShopCardId).Include(x => x.Product).FirstOrDefault();
            //Product product = db.Products.Where(g => g.ProductId == id).FirstOrDefault();
            return PartialView(shopCartItem);
        }

        public ActionResult Order()
        {
            //ShopCartItem shopCartItem = db.ShopCartItems.Where(g => g.Product.ProductId == ProductId && g.ShopCartId == shopCart.ShopCardId).Include(x => x.Product).FirstOrDefault();
            //Product product = db.Products.Where(g => g.ProductId == id).FirstOrDefault();
            return PartialView();
        }
    }
}
