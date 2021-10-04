using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.IO;
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
            ViewBag.OrderId = false;

            //var items = shopCart.getShopItems();   //Список товаров в корзине
            shopCart.listShopItems = shopCart.getShopItems();

            /*var obj = new ShopCartViewModel
            {
                //shopCart = _shopCart
                listShopItems = items
            };*/
            return View(shopCart.listShopItems);
        }

        public RedirectToActionResult UpdateCard(/*int id, int quantity*/)
        {
            //_shopCart.UpdateToCard(id, quantity);
            string[] id = Request.Form["ProductId"];// frc.GetValues("quantity");
            string[] quantity = Request.Form["quantity"];// frc.GetValues("quantity");
            for (int i = 0; i < id.Length; i++)
            {
                int a = Convert.ToInt32(quantity[i]);
                int b = Convert.ToInt32(id[i]);
                shopCart.UpdateToCard(b, a);
            }
            return RedirectToAction("Index");
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

        public ActionResult CreateOrder()
        {
            //ShopCartItem shopCartItem = db.ShopCartItems.Where(g => g.Product.ProductId == ProductId && g.ShopCartId == shopCart.ShopCardId).Include(x => x.Product).FirstOrDefault();
            //Product product = db.Products.Where(g => g.ProductId == id).FirstOrDefault();
            return PartialView();
        }

        public ActionResult OrderOk(string Name, string Patronymic, string LastName, string Phone, string Email)
        {
            var shopCartItems = db.ShopCartItems.Where(x => x.ShopCartId == shopCart.ShopCardId).Include(x => x.Product).ToList();
            Order order = null;
            if (shopCartItems.Count() != 0)
            {
                order = new()
                {
                    ShopCartId = shopCart.ShopCardId,
                    Name = Name,
                    Patronymic = Patronymic,
                    LastName = LastName,
                    Phone = Phone,
                    Email = Email,
                    TotalSum = shopCart.getTotalSumProductCart()
                };
                db.Orders.Add(order);
                db.SaveChanges();

                ShopCart.NewSession();
            }
            ViewBag.ShopCartItem = shopCartItems;
            //Email email = new();
            //email.Send(Email);
            return View(order);
        }
    }
}
