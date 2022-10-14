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
    
    public class MapsController : Controller
    {
        private Context db = new Context();
        private ChangeIDAuto changeIDAuto = new ChangeIDAuto();

        [LoginCheck]
        public ActionResult Index(int page = 1)
        {
            var maps = db.Maps.ToList();
            int pagesize = 10;
            var pagedList = maps.ToPagedList(page, pagesize);
            ViewBag.page = page;
            return View(pagedList);
        }

        [HttpPost]
        public ActionResult Index(string mapsdistrict, int page = 1)
        {
            int pagesize = 10;

            if (mapsdistrict != "" && mapsdistrict != null)
            {
                var mapss = db.Maps.Where(p => p.MapDistrict.Contains(mapsdistrict)).ToList();
                var pagedLists = mapss.ToPagedList(page, pagesize);
                return View(pagedLists);
            }
            var maps = db.Maps.ToList();
            var pagedList = maps.ToPagedList(page, pagesize);
            ViewBag.page = page;
            return View(pagedList);
        }

        // GET: Maps/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maps maps = db.Maps.Find(id);
            if (maps == null)
            {
                return HttpNotFound();
            }
            return PartialView(maps);
        }

        [LoginCheck]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maps/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Maps maps, HttpPostedFileBase photo)
        {
            var idIn = db.Maps.OrderByDescending(m => m.MapID).FirstOrDefault();
            maps.MapID = changeIDAuto.changeIDNumber(idIn.MapID, "K");
            //maps.MapID = "K00001";

            if (photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png")
                    {
                        photo.SaveAs(Server.MapPath("~/MapPhotos/" + maps.MapID + extensionName));

                        maps.MapImg = maps.MapID + extensionName;
                    }
                }
            }

            if (ModelState.IsValid)
            {

                db.Maps.Add(maps);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maps);
        }

        [LoginCheck]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maps maps = db.Maps.Find(id);
            if (maps == null)
            {
                return HttpNotFound();
            }
            return View(maps);
        }

        // POST: Maps/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Maps maps, HttpPostedFileBase photo)
        {
            string sql = "update maps set MapName=@MapName, MapCity=@MapCity, MapDistrict=@MapDistrict, MapAddress=@MapAddress, MapTel=@MapTel, MapInfo=@MapInfo, MapImg=@MapImg where MapID=@MapID";

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PetsMapsCFConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@MapName", maps.MapName);
            cmd.Parameters.AddWithValue("@MapCity", maps.MapCity);
            cmd.Parameters.AddWithValue("@MapDistrict", maps.MapDistrict);
            cmd.Parameters.AddWithValue("@MapAddress", maps.MapAddress);
            cmd.Parameters.AddWithValue("@MapTel", maps.MapTel);
            cmd.Parameters.AddWithValue("@MapInfo", maps.MapInfo);
            cmd.Parameters.AddWithValue("@MapImg", maps.MapImg);
            cmd.Parameters.AddWithValue("@MapID", maps.MapID);

            conn.Open();

            if (photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png")
                    {
                        photo.SaveAs(Server.MapPath("~/MapPhotos/" + maps.MapID + extensionName));
                        maps.MapImg = maps.MapID + extensionName;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                ModelState.Remove("MapImg");
                db.Entry(maps).State = EntityState.Modified;
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
            Maps maps = db.Maps.Find(id);
            if (maps == null)
            {
                return HttpNotFound();
            }
            db.Maps.Remove(maps);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Maps maps = db.Maps.Find(id);
        //    db.Maps.Remove(maps);
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
