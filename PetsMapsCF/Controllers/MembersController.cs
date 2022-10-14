using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetsMapsCF.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.SqlClient;
using System.Configuration;
    
namespace PetsMapsCF.Controllers
{
   [LoginCheck]
    public class MembersController : Controller
    {
        private Context db = new Context();
        private ChangeIDAuto changeIDAuto = new ChangeIDAuto();

        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Members members)
        {
            if (ModelState.IsValid)
            {
                var idIn = db.Members.OrderByDescending(m => m.MemberID).FirstOrDefault();
                members.MemberID = changeIDAuto.changeIDNumber(idIn.MemberID, "M");

                members.CreateDate = DateTime.Now;
                db.Members.Add(members);
                db.SaveChanges();
                
                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    throw;
                //}
                return RedirectToAction("Index");
            }

            return View(members);
        }

        // GET: Members
        public ActionResult Index(int page=1)
        {
            var members = db.Members.ToList();
            int pagesize = 10;
            var pagedList = members.ToPagedList(page, pagesize);
            ViewBag.page = page;
            return View(pagedList);
        }

        [HttpPost]
        public ActionResult Index(string membername, int page = 1)
        {
            int pagesize = 10;

            if (membername != "" && membername != null)
            {
                var memberss = db.Members.Where(p => p.MemberAccount.Contains(membername)).ToList();
                var pagedLists = memberss.ToPagedList(page, pagesize);
                return View(pagedLists);
            }

            var members = db.Members.ToList();
            var pagedList = members.ToPagedList(page, pagesize);
            ViewBag.page = page;
            return View(pagedList);
        }


        // GET: Members/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return PartialView(members);
        }

        // GET: Members/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Members/Create
        //// 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        //// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Members members)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var idIn = db.Members.OrderByDescending(m => m.MemberID).FirstOrDefault();
        //        members.MemberID = changeIDAuto.changeIDNumber(idIn.MemberID, "M");

        //        members.CreateDate = DateTime.Now;
        //        db.Members.Add(members);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(members);
        //}

        // GET: Members/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Members members)
        {
            string sql = "update members set MemberName=@MemberName, MemberGender=@MemberGender, MemberBirthday = @MemberBirthday,MemberMobile=@MemberMobile, MemberAddress=@MemberAddress where MemberID=@MemberID";
            
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PetsMapsCFConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@MemberName", members.MemberName);
            cmd.Parameters.AddWithValue("@MemberGender", members.MemberGender);
            cmd.Parameters.AddWithValue("@MemberBirthday", members.MemberBirthday);
            cmd.Parameters.AddWithValue("@MemberMobile", members.MemberMobile);
            cmd.Parameters.AddWithValue("@MemberAddress", members.MemberAddress);
            cmd.Parameters.AddWithValue("@MemberID", members.MemberID);

            conn.Open();
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

            //if (ModelState.IsValid)
            //{
            //    db.Entry(members).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View(members);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            db.Members.Remove(members);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// POST: Members/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Members members = db.Members.Find(id);
        //    db.Members.Remove(members);
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
