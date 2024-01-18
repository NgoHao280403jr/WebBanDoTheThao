using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using BCrypt;
namespace DoAn.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            DBContext db = new DBContext();
            List<User> users = db.Users.ToList();
            return View(users);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (user != null)
            {
                DBContext db = new DBContext();
                User myUser = db.Users.Where(u => u.Username == user.Username).FirstOrDefault();
                if (myUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(user.Password, myUser.Password))
                    {
                        HttpCookie authCookie = new HttpCookie("auth", myUser.Username);
                        HttpCookie roleCookie = new HttpCookie("role", myUser.Role);
                        HttpCookie IdUserCookie = new HttpCookie("Id",(myUser.UserId).ToString());
                        Response.Cookies.Add(authCookie);
                        Response.Cookies.Add(roleCookie);
                        Response.Cookies.Add(IdUserCookie);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("Password", "Đăng nhập không thành công.");
            }
            ViewBag.IDUSER = user.UserId;
            return View();
        }
        public ActionResult Logout()
        {
            HttpCookie authCookie = new HttpCookie("auth");
            authCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(authCookie);

            HttpCookie roleCookie = new HttpCookie("role");
            roleCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(roleCookie);

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User newUser, string retypePassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (newUser.Password != retypePassword)
            {
                ModelState.AddModelError("retypePassword", "Password không khớp.");
                return View();
            }

            DBContext db = new DBContext();
            User myUser = db.Users.Where(u => u.Username == newUser.Username).FirstOrDefault();
            if (myUser != null)
            {
                ModelState.AddModelError("UserName", "UserName đã tồn tại.");
                return View();
            }

            myUser = db.Users.Where(u => u.EmailAddress == newUser.EmailAddress).FirstOrDefault();
            if (myUser != null)
            {
                ModelState.AddModelError("EmailAddress", "EmailAddress đã tồn tại.");
                return View();
            }

            myUser = new User();
            myUser.Username = newUser.Username;
            myUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            myUser.EmailAddress = newUser.EmailAddress;
            myUser.Role = "user";
            db.Users.Add(myUser);
            db.SaveChanges();

            return RedirectToAction("Login");
        }
        public ActionResult Create()
        {
            DBContext db = new DBContext();
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            DBContext db = new DBContext();
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            DBContext db = new DBContext();
            User user = db.Users.Where(row => row.UserId == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            DBContext db = new DBContext();
            User user1 = db.Users.Where(row => row.UserId == user.UserId).FirstOrDefault();
            user1.Username = user.Username;
            user1.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user1.Role = user.Role;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            DBContext db = new DBContext();
            User user = db.Users.Where(row => row.UserId == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Delete(int id, User user)
        {
            DBContext db = new DBContext();
            User user1 = db.Users.Where(row => row.UserId == id).FirstOrDefault();
            db.Users.Remove(user1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}