using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class NewsController : Controller
    {
        private DatabaseContext db;
        public NewsController(DatabaseContext _db)
        {
            db = _db;
        }

        public IActionResult Index(int id)
        {
            News news = db.News.First(i => i.Id == id);
            return View(news);
        }
    }
}
