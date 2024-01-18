using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
namespace DoAn.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index(int IdUser)
        {
            DBContext db = new DBContext();
            List<Cart> carts = db.Cart.Where(x => x.UserId == IdUser).ToList();
            return View(carts);
        }
        public ActionResult Add(int id ,int idUser)
        {
            if (id > 0)
            {
                DBContext db = new DBContext();
                Cart cartItem = db.Cart.Where(row => row.ProductId == id).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    Cart cart = new Cart();
                    cart.ProductId = id;
                    cart.UserId = idUser;
                    cart.Quantity = 1;
                    db.Cart.Add(cart);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index",new {IdUser=idUser});
        }

        public ActionResult UpdateQuantity(int quan, int proid,int idUser)
        {
            DBContext db = new DBContext();
            if (quan > 0)
            {
                Cart cartItem = db.Cart.Where(cart => cart.ProductId == proid).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity = quan;
                    cartItem.UserId = idUser;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index",new { IdUser = idUser });
        }
        public ActionResult Delete(int id, int idUser)
        {
            DBContext db = new DBContext();
            Cart prd = db.Cart.Where(row => row.CartId == id).FirstOrDefault();
            prd.UserId = idUser;
            db.Cart.Remove(prd);
            db.SaveChanges();
            return RedirectToAction("Index", new { IdUser = idUser });
        }
    }
}