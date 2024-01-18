using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
namespace DoAn.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string search = "", string sortCol = "Price", string cheDo = "asc", int page = 1)
        {
            DBContext db = new DBContext();
            List<Product> products = db.Products.Where(x => x.ProductName.Contains(search)).ToList();
            List<Brand> brands = db.Brands.ToList();
            ViewBag.brands = brands;
            List<Category> categories = db.Categories.ToList();
            ViewBag.categories = categories;
            if (sortCol == "Price")
            {
                if (cheDo == "asc")
                {
                    products = products.OrderBy(x => x.Price).ToList();
                }
                else
                {
                    products = products.OrderByDescending(x => x.Price).ToList();
                }
            }
            ViewBag.sortCol = sortCol;
            ViewBag.cheDo = cheDo;
            int soDongMoiTrang = 5;
            int tongSoTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(soDongMoiTrang)));
            int skip = (page - 1) * soDongMoiTrang;
            products = products.Skip(skip).Take(soDongMoiTrang).ToList();
            ViewBag.page = page;
            ViewBag.tongST = tongSoTrang;
            return View(products);
        }
        public ActionResult sbar(int id, string search = "", string sortCol = "Price", string cheDo = "asc", int page = 1)
        {
            DBContext db = new DBContext();
            List<Product> products = db.Products.Where(x => x.BrandId == id).ToList();
            List<Brand> brands = db.Brands.ToList();
            ViewBag.brands = brands;
            DBContext db1 = new DBContext();
            List<Category> categories = db1.Categories.ToList();
            ViewBag.categories = categories;
            ViewBag.search = search;
            ViewBag.search = search;
            if (sortCol == "Price")
            {
                if (cheDo == "asc")
                {
                    products = products.OrderBy(x => x.Price).ToList();
                }
                else
                {
                    products = products.OrderByDescending(x => x.Price).ToList();
                }
            }
            ViewBag.sortCol = sortCol;
            ViewBag.cheDo = cheDo;
            int soDongMoiTrang = 3;
            int tongSoTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(soDongMoiTrang)));
            int skip = (page - 1) * soDongMoiTrang;
            products = products.Skip(skip).Take(soDongMoiTrang).ToList();
            ViewBag.page = page;
            ViewBag.tongST = tongSoTrang;
            ViewBag.id = id;
            return View(products);
        }
        public ActionResult sbar1(int id, string search = "", string sortCol = "Price", string cheDo = "asc", int page = 1)
        {
            DBContext db = new DBContext();
            List<Product> products = db.Products.Where(x => x.CatelogyId == id).ToList();
            List<Category> categories = db.Categories.ToList();
            ViewBag.categories = categories;
            DBContext db1 = new DBContext();
            List<Brand> brands = db1.Brands.ToList();
            ViewBag.brands = brands;
            ViewBag.search = search;
            if (sortCol == "Price")
            {
                if (cheDo == "asc")
                {
                    products = products.OrderBy(x => x.Price).ToList();
                }
                else
                {
                    products = products.OrderByDescending(x => x.Price).ToList();
                }
            }
            ViewBag.sortCol = sortCol;
            ViewBag.cheDo = cheDo;
            int soDongMoiTrang = 3;
            int tongSoTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(soDongMoiTrang)));
            int skip = (page - 1) * soDongMoiTrang;
            products = products.Skip(skip).Take(soDongMoiTrang).ToList();
            ViewBag.page = page;
            ViewBag.tongST = tongSoTrang;
            ViewBag.id = id;
            return View(products);
        }
        public ActionResult detail(int id)
        {
            DBContext db = new DBContext();
            List<Product> products = db.Products.ToList();
            ViewBag.products = products;
            Product pro = db.Products.Where(p => p.ProductID == id).FirstOrDefault();
            return View(pro);
        }
        
        public ActionResult Create()
        {
            DBContext db = new DBContext();
            ViewBag.brands = db.Brands.ToList();
            ViewBag.categories = db.Categories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            DBContext db = new DBContext();
            db.Products.Add(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            DBContext db = new DBContext();
            ViewBag.brands = db.Brands.ToList();
            ViewBag.categories = db.Categories.ToList();
            Product pr = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return View(pr);
        }
        [HttpPost]
        public ActionResult Edit(Product pr)
        {
            DBContext db = new DBContext();           
            Product emp = db.Products.Where(row => row.ProductID == pr.ProductID).FirstOrDefault();
            emp.ProductName = pr.ProductName;
            emp.Price = pr.Price;
            emp.DateOfPurchase = pr.DateOfPurchase;
            emp.AvailabilityStatus = pr.AvailabilityStatus;
            emp.Gender = pr.Gender;
            emp.Size = pr.Size;
            emp.Image= pr.Image;
            emp.BrandId = pr.BrandId;
            emp.CatelogyId = pr.CatelogyId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            DBContext db = new DBContext();
            Product pr = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return View(pr);
        }
        [HttpPost]
        public ActionResult Delete(int id,Product pr)
        {
            DBContext db = new DBContext();
            Product prd = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            db.Products.Remove(prd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}