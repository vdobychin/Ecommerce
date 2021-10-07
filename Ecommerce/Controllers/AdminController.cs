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
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
   // [Authorize]
    public class AdminController : Controller
    {
        private DatabaseContext db;
        private ShopCart shopCart;
        public AdminController(DatabaseContext _db, ShopCart _shopCart)
        {
            db = _db;
            shopCart = _shopCart;
        }

        [Authorize(Roles = "admin")]
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
            string salt = db.Registrations.Where(x => x.User.Name == loginViewModel.registration.User.Name).Select(x => x.Salt).FirstOrDefault();
            string hash = db.Registrations.Where(x => x.User.Name == loginViewModel.registration.User.Name).Select(x => x.Hash).FirstOrDefault();
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
                    new Claim("UserName", loginViewModel.registration.User.Name),
                    new Claim(ClaimTypes.NameIdentifier, loginViewModel.registration.User.Name)
                };

            if(db.Registrations.First(x => x.Role == "admin") != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string ReturnUrl)
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)//(string ReturnUrl, string Name, string Patronymic, string LastName, string Phone, string Email, string Password)
        {
            //Проверка, что такого пользователя нет
            User userExists = new();
            userExists = db.Users.First(x => x.Phone == registerViewModel.User.Phone || x.Email == registerViewModel.User.Email);
            if (userExists is not null)
            {                
                return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>"); //Register("");// PartialView();
            }

            

            User user = new()
            {
                Name = registerViewModel.User.Name,
                Patronymic = registerViewModel.User.Patronymic,
                LastName = registerViewModel.User.LastName,
                Phone = registerViewModel.User.Phone,
                Email = registerViewModel.User.Email
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

            Registration registration = new()
            {
                Salt = Convert.ToBase64String(salt),
                Hash = hashed,
                Role = "user",
                UserId = user.Id
            };

            db.Users.Add(user);
            db.Registrations.Add(registration);
            db.SaveChanges();

            return Redirect("/Home/Index");
        }
    }
}
