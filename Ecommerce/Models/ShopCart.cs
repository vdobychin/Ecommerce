using Ecommerce.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class ShopCart
    {
        private readonly DatabaseContext db;
        public ShopCart(DatabaseContext _databaseContext)
        {
            db = _databaseContext;
        }

        public string ShopCardId { get; set; }
        //public List<ShopCartItem> listShopItems { get; set; }
        public ICollection<ShopCartItem> listShopItems { get; set; }

        static IServiceProvider _services;
        public static ShopCart GetCart(IServiceProvider services)
        {
            _services = services;
            //ISession - класс для работы с сессиями
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session; // Создали сессию
            var context = services.GetService<DatabaseContext>();
            string shopCartId = session.GetString("MyId") ?? Guid.NewGuid().ToString();
            session.SetString("MyId", shopCartId);

            return new ShopCart(context) { ShopCardId = shopCartId };
        }
        public static void NewSession()
        {
            ISession session = _services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session; // Создали сессию
            session.Clear();
            var context = _services.GetService<DatabaseContext>();
            string shopCartId = session.GetString("MyId") ?? Guid.NewGuid().ToString();
            session.SetString("MyId", shopCartId);
            new ShopCart(context) { ShopCardId = shopCartId };
        }

        public void AddToCart(Product product, int quantity)
        {
            ShopCartItem shopCartItem = db.ShopCartItems.Where(g => g.Product.ProductId == product.ProductId && g.ShopCartId == ShopCardId && g.Time.Date == DateTime.Today).FirstOrDefault();
            if (shopCartItem == null)
            {
                db.ShopCartItems.Add(new ShopCartItem
                {
                    Time = DateTime.Now,
                    ShopCartId = ShopCardId,
                    Product = product,
                    Price = product.Price,
                    Quantity = quantity
                });
            }
            else
            {
                shopCartItem.Quantity += quantity;
            }
            db.SaveChanges();
        }

        public void DeleteToCard(int id)
        {
            ShopCartItem shopCartItem;
            shopCartItem = db.ShopCartItems.Where(g => g.Id == id && g.ShopCartId == ShopCardId).FirstOrDefault();
            db.ShopCartItems.Remove(shopCartItem);
            db.SaveChanges();
        }

        public void UpdateToCard(int id, int quantity)
        {
            ShopCartItem shopCartItem;
            //shopCartItem = appDBContent.ShopCartItem.FirstOrDefault(x => x.Id == id);
            shopCartItem = db.ShopCartItems.Where(g => g.Product.ProductId == id && g.ShopCartId == ShopCardId).FirstOrDefault();
            shopCartItem.Quantity = quantity;
            db.ShopCartItems.Update(shopCartItem);
            db.SaveChanges();
        }

        public List<ShopCartItem> getShopItems()    //Список товаров в корзине
        {
            return db.ShopCartItems.Where(a => a.ShopCartId == ShopCardId).Include(a => a.Product).ToList();
        }

        public int getTotalQuantityProductCart()
        {
            return db.ShopCartItems.Where(a => a.ShopCartId == ShopCardId/*текущий Id сессии*/).Sum(a => a.Quantity);
        }

        public decimal getTotalSumProductCart()
        {
            return db.ShopCartItems.Where(a => a.ShopCartId == ShopCardId/*текущий Id сессии*/).ToList().Sum(a => a.Quantity * a.Price);
        }
    }
}
