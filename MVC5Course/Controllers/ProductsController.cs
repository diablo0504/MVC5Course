using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        ProductRepository repo = RepositoryHelper.GetProductRepository();
        // GET: Products
        [OutputCache(Duration =5,Location =System.Web.UI.OutputCacheLocation.ServerAndClient)]
        public ActionResult Index(bool Active =true)
        {
            //return View(db.Product.OrderByDescending(p => p.ProductId).Take(10));
            var data = repo.GetProduct列表頁所有資料(Active, showAll: false);
            //var data = repo.All()
            //     .Where(p => p.Active.HasValue && p.Active.Value == Active)
            //     .OrderByDescending(p => p.ProductId).Take(10);
            ViewData.Model = data;
            ViewData["ppp"] = data;
            ViewBag.qqq = data;
           return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {//Modelbinding 模型繫節
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error_DbUpdateException")]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            //if (ModelState.IsValid)
           // {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
           // }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,FormCollection form)
        {
            // [Bind(Include = "ProductId,ProductName,Price,Active,Stock")]
            //  Product product
            var product = repo.Get單筆資料ByProductId(id);
            
            if (TryUpdateModel<Product>(product,new string[] { "ProductId","ProductName","Price","Active","Stock" }))
            {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
                //repo.Update(product);
                repo.UnitOfWork.Commit();
                
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            Product product = repo.Get單筆資料ByProductId(id);
            repo.Delete(product);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        public ActionResult ListProducts(ProductListSearchVM searchCondition)
        {
            var data = repo.GetProduct列表頁所有資料(true);

            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(searchCondition.q))
                {
                    data = data.Where(p => p.ProductName.Contains(searchCondition.q));
                }

                data = data.Where(p => p.Stock > searchCondition.Stock_S && p.Stock < searchCondition.Stock_E);
            }

            ViewData.Model = data
                .Select(p => new ProductLiteVM()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Stock = p.Stock
                });

            return View();
        }
        //public ActionResult ListProducts(string q ,int stock_s =0 ,int stock_e = 99999)
        //{
        //    var data = repo.GetProduct列表頁所有資料(true);

        //    if (!String.IsNullOrEmpty(q))
        //    {
        //        data = data.Where(p => p.ProductName.Contains(q));
        //    }
        //        data = data.Where(p => p.Stock >= stock_s && p.Stock <= stock_e); 
        //        ViewData.Model = data
        //        .Select(p => new ProductLiteVM()
        //        {
        //            ProductId = p.ProductId,
        //            ProductName = p.ProductName,
        //            Price = p.Price,
        //            Stock = p.Stock
        //        });

        //    return View();
        //}

        public ActionResult BatchUpdate()
        {
            return View();
        }





        public ActionResult CreatProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatProduct([Bind(Include = "ProductId,ProductName,Price,Active,Stock")]ProductLiteVM data)
        {

            if (ModelState.IsValid)
            {
                // TODO: 儲存資料進資料庫
                TempData["CreatProduct"] = "商品新增成功";
                return RedirectToAction("ListProducts");
            }
            //// 驗證失敗，繼續顯示原本的表單
            return View();
        }

    }
}
