using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetsMapsCF.Models;
using System.Data.SqlClient;
using System.Configuration;
using PagedList;
using PagedList.Mvc;

namespace PetsMapsCF.Controllers
{
    
    public class ProductsController : Controller
    {
        private Context db = new Context();
        private ChangeIDAuto changeIDAuto = new ChangeIDAuto();

        [LoginCheck]
        public ActionResult Index(int page = 1)
        {
            var products = db.Products.ToList();
            int pagesize = 10;
            var pagedList = products.ToPagedList(page, pagesize);
            ViewBag.page = page;
            return View(pagedList);
        }

        [HttpPost]
        public ActionResult Index(string productsname, int page = 1)
        {
            int pagesize = 10;

            if (productsname != "" && productsname != null)
            {
                var productss = db.Products.Where(p => p.ProductName.Contains(productsname)).ToList();
                var pagedLists = productss.ToPagedList(page, pagesize);
                return View(pagedLists);
            }
            var products = db.Products.ToList();

            var pagedList = products.ToPagedList(page, pagesize);
            ViewBag.page = page;
            return View(pagedList);
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return PartialView(products);
        }

        [LoginCheck]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products, HttpPostedFileBase photo)
        {
            var idIn = db.Products.OrderByDescending(p => p.ProductID).FirstOrDefault();
            products.ProductID = changeIDAuto.changeIDNumber(idIn.ProductID, "P");

            if (photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png" || extensionName == ".jpeg")
                    {
                        photo.SaveAs(Server.MapPath("~/ProductPhotos/" + products.ProductID + extensionName));

                        products.ProductImg = products.ProductID + extensionName;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(products);
        }

        [LoginCheck]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products products, HttpPostedFileBase photo)
        {
            string sql = "update products set ProductName=@ProductName, UnitPrice=@UnitPrice, ProductImg=@ProductImg where ProductID=@ProductID";

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PetsMapsCFConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ProductName", products.ProductName);
            cmd.Parameters.AddWithValue("@UnitPrice", products.UnitPrice);
            cmd.Parameters.AddWithValue("@ProductImg", products.ProductImg);
            cmd.Parameters.AddWithValue("@ProductID", products.ProductID);

            conn.Open();

            if (photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png" || extensionName == ".jpeg")
                    {
                        photo.SaveAs(Server.MapPath("~/ProductPhotos/" + products.ProductID + extensionName));
                        products.ProductImg = products.ProductID + extensionName;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                ModelState.Remove("ProductImg");
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            try
            {
                cmd.ExecuteNonQuery(); /*很容易發生例外所以使用try catch*/
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            conn.Close();

            return RedirectToAction("Index");
        }

        [LoginCheck]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Products products = db.Products.Find(id);
        //    db.Products.Remove(products);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
