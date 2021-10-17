using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private DatabaseContext db;
        private ShopCart shopCart;
        public AdminController(DatabaseContext _db, ShopCart _shopCart)
        {
            db = _db;
            shopCart = _shopCart;
        }

        //[Authorize(Roles = "admin")]
       // public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Denied()
        {
            return View();
        }

        [HttpGet]
        //[AllowAnonymous] //Сюда попадут не авторизованные пользователи
        public IActionResult Login()
        {
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();

            return View();
        }

        
        [HttpPost]
        //[AllowAnonymous] //Сюда попадут не авторизованные пользователи
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();

            var regParam = db.Credentials.Include(x => x.User).FirstOrDefault(x => x.User.Email == loginViewModel.Email);            
            if (regParam is null)
            {
                loginViewModel.IsValidResponse = "Такого пользователя не существует.";
                return View(loginViewModel);
            }

            /*if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }*/

            if (!loginViewModel.IsPasswordValid(loginViewModel.Password, regParam.Salt, regParam.Hash))
            {
                loginViewModel.IsValidResponse = "Неверный логин.";
                return View(loginViewModel);
            }

            AddClaim(regParam.User);

            return Redirect(loginViewModel.ReturnUrl);
        }

        async void AddClaim(User user)
        {
            var regParam = db.Credentials.SingleOrDefault(x => x.User.Email == user.Email);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Name)
            };

            if (regParam.Role == "admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));
            //await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookie")));
        }

        public IActionResult LogOff()
        {
            HttpContext.SignOutAsync("Cookie");
            return Redirect("/Home/Index");
        }

        
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)//(string ReturnUrl, string Name, string Patronymic, string LastName, string Phone, string Email, string Password)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }

            string phone = new string(registerViewModel.User.Phone.Where(char.IsDigit).ToArray());
            //Проверка, что такого пользователя нет            
            //if (db.Users.Where(x => x.Phone == phone || x.Email == registerViewModel.User.Email).Any())
            //    return BadRequest();


            User user = new()
            {
                Name = registerViewModel.User.Name,
                Patronymic = registerViewModel.User.Patronymic,
                LastName = registerViewModel.User.LastName,
                Phone = phone,
                Email = registerViewModel.User.Email,
                CreateTime = DateTime.Today
            };

            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())  //криптографический генератор случайных чисел
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(    //алгоритм PBKDF2
            password: registerViewModel.Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
            
            db.Users.Add(user);
            db.SaveChanges();

            Credential credential = new()
            {
                Salt = Convert.ToBase64String(salt),
                Hash = hashed,
                Role = "user",
                UserId = user.Id
            };
                        
            db.Credentials.Add(credential);
            db.SaveChanges();
            AddClaim(user);

            return Redirect("/Home/Index");
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckRegister(FeedbackViewModel feedbackViewModel)
        {
            if (feedbackViewModel.RegisterViewModel.User.Phone is not null)
                feedbackViewModel.RegisterViewModel.User.Phone = new string(feedbackViewModel.RegisterViewModel.User.Phone.Where(char.IsDigit).ToArray());

            if (db.Users.Where(x => x.Phone == feedbackViewModel.RegisterViewModel.User.Phone || x.Email == feedbackViewModel.RegisterViewModel.User.Email).Any())
                return Json(false);
            return Json(true);
        }

        public IActionResult Privacy()
        {
            ViewBag.TotalQuantity = shopCart.getTotalQuantityProductCart();
            ViewBag.TotalSum = shopCart.getTotalSumProductCart();
            return View();
        }
    }
}
