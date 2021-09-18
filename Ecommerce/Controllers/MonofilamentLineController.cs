using Ecommerce.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class MonofilamentLineController : Controller
    {
        private DatabaseContext db;

        public MonofilamentLineController(DatabaseContext _db)
        {
            db = _db;
        }

        public ActionResult Index()
        {
            ViewBag.products = db.Products.ToList();
            return PartialView();
        }
    }
}
