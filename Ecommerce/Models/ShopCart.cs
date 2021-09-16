﻿using Ecommerce.Data;
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
        public List<ShopCartItem> listShopItems { get; set; }

        public static ShopCart GetCart(IServiceProvider services)
        {
            //ISession - класс для работы с сессиями
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session; // Создали сессию
            var context = services.GetService<DatabaseContext>();
            string shopCartId = session.GetString("MyId") ?? Guid.NewGuid().ToString();
            session.SetString("MyId", shopCartId);

            return new ShopCart(context) { ShopCardId = shopCartId };
        }
        
        public void AddToCart(Product product, int quantity)
        {
            ShopCartItem shopCartItem = db.ShopCartItems.Where(g => g.ProductId == product.Id && g.ShopCartId == ShopCardId).FirstOrDefault();
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

        public List<ShopCartItem> getShopItems()    //Список товаров в корзине
        {
            return db.ShopCartItems.Where(c => c.ShopCartId == ShopCardId).Include(s => s.Product).ToList();
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