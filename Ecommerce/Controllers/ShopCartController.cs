using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
using System.Security.Claims;

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

        [Authorize]
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
            FeedbackViewModel feedbackViewModel = new() { ShopCartItems = shopCart.listShopItems };
            //return View(shopCart.listShopItems);
            return View(feedbackViewModel);
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


        [Authorize]
        public ActionResult OrderOk(string deliveryRadioDefault, string City, string AddressDelivery, string paymentRadioDefault)
        {
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();

            var shopCartItems = db.ShopCartItems.Where(x => x.ShopCartId == shopCart.ShopCardId).Include(x => x.Product).ToList();
            FeedbackViewModel feedbackViewModel = new();
            feedbackViewModel.Order = new()
            {
                ShopCartId = shopCart.ShopCardId,
                Delivery = deliveryRadioDefault,
                City = (deliveryRadioDefault == "Доставка" ? City : null),
                AddressDelivery = (deliveryRadioDefault == "Доставка" ? AddressDelivery : null),
                Payment = paymentRadioDefault,
                TotalSum = shopCart.getTotalSumProductCart(),
                CreateTime = DateTime.Today,
                User = db.Users.First(x => x.Email == User.FindFirstValue(ClaimTypes.Email))
            };
            db.Orders.Add(feedbackViewModel.Order);
            db.SaveChanges();
            ShopCart.NewSession();
            
            ViewBag.ShopCartItem = shopCartItems;
            //Email email = new();
            //email.Send(Email);
            
            return View(feedbackViewModel);
        }
    }
}
