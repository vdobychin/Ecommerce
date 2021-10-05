using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private DatabaseContext db;
        private ShopCart shopCart;
        public AdminController(DatabaseContext _db, ShopCart _shopCart)
        {
            db = _db;
            shopCart = _shopCart;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Denied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous] //Сюда попадут не авторизованные пользователи
        public IActionResult Login(string ReturnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous] //Сюда попадут не авторизованные пользователи
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            string salt = db.Logins.Where(x => x.UserName == loginViewModel.login.UserName).Select(x => x.Salt).FirstOrDefault();
            string hash = db.Logins.Where(x => x.UserName == loginViewModel.login.UserName).Select(x => x.Hash).FirstOrDefault();
            if (String.IsNullOrEmpty(salt) && String.IsNullOrEmpty(hash))
            {
                //return View(loginViewModel);
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                //return View(loginViewModel);
                return BadRequest();
            }
            
            if (!loginViewModel.IsPasswordValid(loginViewModel.Password, salt, hash))
            {
                return BadRequest();
                //return View(loginViewModel);
            }

            var claims = new List<Claim>
                {
                    new Claim("UserName", loginViewModel.login.UserName),
                    new Claim(ClaimTypes.NameIdentifier, loginViewModel.login.UserName)
                };

            if(db.Logins.First(x => x.Role == "Admin") != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));
            //await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookie")));
            return Redirect(loginViewModel.ReturnUrl);
        }


        public IActionResult LogOff()
        {
            HttpContext.SignOutAsync("Cookie");
            return Redirect("/Home/Index");
        }
    }
}
