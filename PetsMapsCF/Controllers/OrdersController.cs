using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetsMapsCF.Models;
using System.Data.SqlClient;
using PagedList;
using PagedList.Mvc;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace PetsMapsCF.Controllers
{
    public class OrdersController : Controller
    {
        private Context db = new Context();

        [ChildActionOnly]
        public ActionResult _Order()
        {
            ViewBag.PayTypeID = new SelectList(db.PayTypes, "PayTypeID", "PayTypeName");
            ViewBag.ShipID = new SelectList(db.Shippers, "ShipID", "ShipVia");
            return View();
        }

        [LoginCheck]
        public ActionResult OrderList(int page = 1)
        {
            var orders = db.GroupBuyingOrders.Include(g => g.Member).Include(g => g.PayType).Include(g => g.Shipper).ToList();
            int pagesize = 10;
            var pagedList = orders.ToPagedList(page, pagesize);
            ViewBag.page = page;

            return View(pagedList);
        }

        [HttpPost]
        public ActionResult OrderList(string ordersid, int page=1)
        {
            int pagesize = 10;

            if (ordersid != "" && ordersid != null)
            {
                var orderss = db.GroupBuyingOrders.Where(p => p.OrderID.Contains(ordersid)).ToList();
                var pagedLists = orderss.ToPagedList(page, pagesize);
                return View(pagedLists);
            }

            var ordersss = db.GroupBuyingOrders.ToList();
            var pagedList = ordersss.ToPagedList(page, pagesize);
            ViewBag.page = page;
            return View(pagedList);
        }

        public ActionResult Details(string id)
        {
            var orders = db.GroupBuyingOrders.Where(p => p.OrderID == id).ToList();

            return PartialView(orders);
        }

        [ChildActionOnly]
        public ActionResult _OrderDetail(string id)
        {
            var orderdetail = db.OrderDetails.Where(p => p.OrderID == id).ToList();

            ViewBag.subtotal = orderdetail.Count;
            return View(orderdetail);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupBuyingOrders groupBuyingOrders = db.GroupBuyingOrders.Find(id);
            if (groupBuyingOrders == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "MemberAccount", groupBuyingOrders.MemberID);
            ViewBag.PayTypeID = new SelectList(db.PayTypes, "PayTypeID", "PayTypeName", groupBuyingOrders.PayTypeID);
            ViewBag.ShipID = new SelectList(db.Shippers, "ShipID", "ShipVia", groupBuyingOrders.ShipID);
            return View(groupBuyingOrders);
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupBuyingOrders groupBuyingOrders)
        {
            //string sql = "update groupBuyingOrders set Consignee=@Consignee, ShipAddress=@ShipAddress, @ShipDate=@ShipDate, @DeliveryDate=@DeliveryDate where OrderID=@OrderID";

            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PetsMapsCFConnection"].ConnectionString);
            //SqlCommand cmd = new SqlCommand(sql, conn);

            //cmd.Parameters.AddWithValue("@Consignee", groupBuyingOrders.Consignee);
            //cmd.Parameters.AddWithValue("@ShipAddress", groupBuyingOrders.ShipAddress);
            //cmd.Parameters.AddWithValue("@ShipDate", groupBuyingOrders.ShipDate);
            //cmd.Parameters.AddWithValue("@DeliveryDate", groupBuyingOrders.DeliveryDate);
            //cmd.Parameters.AddWithValue("@OrderID", groupBuyingOrders.OrderID);

            //conn.Open();
            //try
            //{
            //    cmd.ExecuteNonQuery(); /*很容易發生例外所以使用try catch*/
            //    return RedirectToAction("OrderList");
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Error = ex.Message;
            //}


            if (ModelState.IsValid)
            {
                db.Entry(groupBuyingOrders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OrderList", "Orders");
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "MemberAccount", groupBuyingOrders.MemberID);
            ViewBag.PayTypeID = new SelectList(db.PayTypes, "PayTypeID", "PayTypeName", groupBuyingOrders.PayTypeID);
            ViewBag.ShipID = new SelectList(db.Shippers, "ShipID", "ShipVia", groupBuyingOrders.ShipID);

            return View(groupBuyingOrders);
        }

        public ActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Order(string Consignee, string ShipAddress, string ShipID, string PayTypeID, string data)
        {

            //如果沒登入,導去登入
            if (Session["member"] == null)
                return RedirectToAction("Login", "Home");

            var memberID = ((Members)Session["member"]).MemberID;

            //如果已登入,就寫入資料庫
            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("Consignee",Consignee),
                new SqlParameter("ShipAddress",ShipAddress),
                new SqlParameter("ShipID",ShipID),
                new SqlParameter("PayTypeID",PayTypeID),
                new SqlParameter("MemberID",memberID),
                new SqlParameter("cart",data)
            };
            SetData sd = new SetData();
            sd.executeSqlBySP("addOrders", list);

            TempData["cartFlag"] = "OK";

            return RedirectToAction("MyCart", "Home");
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupBuyingOrders orders = db.GroupBuyingOrders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            db.GroupBuyingOrders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("OrderList");
        }

      



    }
}