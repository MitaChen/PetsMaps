using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetsMapsCF.Models;

namespace PetsMapsCF.Controllers
{

  
    public class HomeManagerController : Controller
    {
        Context db = new Context();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(VMLogin vMLogin)
        {
            var user = db.Administrators.Where(m => m.AdminID == vMLogin.AdminID && m.AdminPassword == vMLogin.AdminPassword).FirstOrDefault();

            if (user == null)
            {
                ViewBag.ErrMsg = "帳號或密碼有誤，請重新輸入。";
                return View(vMLogin);
            }

            Session["user"] = user;
            return RedirectToAction("Index","HomeManager");

        }


        public ActionResult Logout()
        {

            Session["user"] = null;
            return RedirectToAction("Index", "HomeManager");
        }
    }
}