using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
namespace DoAn.Controllers
{
    public class PayController : Controller
    {
        // GET: Pay
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(Pay pay)
        {
            DBContext db = new DBContext();
            db.Pays.Add(pay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}