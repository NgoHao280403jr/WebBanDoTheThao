using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            DBContext db = new DBContext();
            List<Product> products = db.Products.ToList();
            
            return View(products);
        }
        public ActionResult ChuaLogIn()
        {
            return View();
        }
    }
}