using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : BaseController
    {       
        
        // GET: EF
        public ActionResult Index()
        {
            var all = db.Product.AsQueryable();

            var data = all
              .Where(p => p.Active == true && p.ProductName.Contains("Black"))
              .Where(p => p.Is刪除 == false && p.Active == true && p.ProductName.Contains("Black"))
                 .OrderByDescending(p => p.ProductId);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p )
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id);
            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(int id,Product Product)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                item.ProductName = Product.ProductName;
                item.Price = Product.Price;
                item.Stock = Product.Stock;
                item.Active = Product.Active;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            /*
            foreach (var item in product.OrderLine)
            {
                db.OrderLine.Remove(item);
            }
            */
            db.OrderLine.RemoveRange(product.OrderLine);
            //db.OrderLine.RemoveRange(product.OrderLine);
            //db.Product.Remove(product);
            product.Is刪除 = true;


            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(int? id)
        //{
        //    var item = db.Product.Find(id);
        //    return View(item);
        //}
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    Product product = db.Product.Find(id);
        //    db.Product.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult Details(int id)
        {
            var data = db.Database.SqlQuery<Product>(
                "select * from dbo.Product where ProductId=@p0", id).FirstOrDefault();
            return View(data);
        }
    }
}