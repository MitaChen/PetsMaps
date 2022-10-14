using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetsMapsCF.Models;
using PagedList;
using PagedList.Mvc;


namespace PetsMapsCF.Controllers
{
    public class HomeController : Controller
    {
        Context db = new Context();
        private ChangeIDAuto changeIDAuto = new ChangeIDAuto();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Members members)
        {
            var Account = db.Members.Where(m=>m.MemberAccount == members.MemberAccount).FirstOrDefault();
            if(Account != null)
            {
                TempData["AccountNull"] = "帳號已有人使用。";
                return View();
            }
            if (ModelState.IsValid)
            {
                var idIn = db.Members.OrderByDescending(m => m.MemberID).FirstOrDefault();
                members.MemberID = changeIDAuto.changeIDNumber(idIn.MemberID, "M");

                members.CreateDate = DateTime.Now;
                db.Members.Add(members);
                db.SaveChanges();

                TempData["registersu"] = "註冊成功！請登入會員。";
                ViewBag.registersu = "註冊成功！請登入會員。";

                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult ProductList(int page = 1)
        {
            var products = db.Products.ToList();
            int pagesize = 6;
            var pagedList = products.ToPagedList(page, pagesize);
            ViewBag.page = page;

            return View(pagedList);
        }

        [HttpPost]
        public ActionResult ProductList(string productsname, int page = 1)
        {
            int pagesize = 6;

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


        public ActionResult MyCart()
        {

            return View();
        }


        public ActionResult MapList(int page = 1)
        {

            var maps = db.Maps.ToList();
            int pagesize = 6;
            var pagedList = maps.ToPagedList(page, pagesize);
            ViewBag.page = page;
            return View(pagedList);

        }

        [HttpPost]
        public ActionResult MapList(string name, int page = 1)
        {
            int pagesize = 6;

            if (name != "" && name != null)
            {
                var mapssss = db.Maps.Where(k => k.MapDistrict.Contains(name)).ToList();
                var pagedLists = mapssss.ToPagedList(page, pagesize);
                return View(pagedLists);
            }

            var maps = db.Maps.ToList();
            var pagedList = maps.ToPagedList(page, pagesize);
            ViewBag.page = page;

            return View(pagedList);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemVMLogin memVMLogin)
        {
            string pw = Members.getHashPassword(memVMLogin.MemberPassword);
            var member = db.Members.Where(m => m.MemberAccount == memVMLogin.MemberAccount && m.MemberPassword == pw).FirstOrDefault();

            if (member == null)
            {
         
                ViewBag.ErrMsg = "帳號或密碼輸入錯誤，請重新輸入";
                return View(memVMLogin);
            }

            Session["member"] = member;
            TempData["message"] = "登入成功！";
            return RedirectToAction("ProductList");

        }

        public ActionResult Logout()
        {
            Session["member"] = null;
            TempData["message"] = "已成功登出！";
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetMemVMLogin forgetMemVMLogin)
        {

            var account = db.Members.Where(m => m.MemberAccount == forgetMemVMLogin.MemberAccount && m.MemberBirthday == forgetMemVMLogin.MemberBirthday && m.MemberMobile == forgetMemVMLogin.MemberMobile).FirstOrDefault();

            if (account == null)
            {
                ViewBag.accnull = "帳號、生日或手機驗證錯誤，請重新輸入。";
                return View(forgetMemVMLogin);
            }

            Members members = db.Members.Find(forgetMemVMLogin.MemberAccount);
            
            if (ModelState.IsValid)
            {
                account.MemberPassword =forgetMemVMLogin.MemberPassword;
                db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                TempData["pweditsu"] = "密碼修改完成，請重新登入。";
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [ChildActionOnly]
        public ActionResult _MemberDetail()
        {
            var memberID = ((Members)Session["member"]).MemberID;
            var memberlist = db.Members.Where(p => p.MemberID == memberID).ToList();
            return View(memberlist);
        }

        public ActionResult OrderList()
        {
            if (Session["member"] == null)
                return RedirectToAction("Login", "Home");

            var memberID = ((Members)Session["member"]).MemberID;
            var orderlist = db.GroupBuyingOrders.Where(p => p.MemberID == memberID).OrderByDescending(p => p.OrderDate).ToList();
            return View(orderlist);

        }

        public ActionResult OrderDetail(string id)
        {
            var orderlist = db.OrderDetails.Where(p => p.OrderID == id).ToList();

            ViewBag.subtotal=orderlist.Count;
            return View(orderlist);
        }



    }

}