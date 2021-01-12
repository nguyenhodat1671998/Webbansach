using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuaHangSach.Action;
using System.Net;
using System.Net.Mail;
using System.IO;
using WebCuaHangSach.Models;
using Common;

namespace WebCuaHangSach.Controllers
{
    public class CashController : Controller
    {

        #region CashManagement
        public ActionResult ListNotApply()
        {
            ViewBag.ListNotApply = CashAction.ListNotApply();
            return View();
        }

        public ActionResult Apply(int ID)
        {
            CashAction.Apply(ID);
            return RedirectToAction("ListNotApply");
        }

        public ActionResult RemoveBill(int ID)
        {
            CashAction.RemoveBill(ID);
            return RedirectToAction("ListNotApply");
        }

        public ActionResult ListApply()
        {
            ViewBag.ListApply = CashAction.ListApply();
            return View();
        }

        public ActionResult Paid(int ID)
        {
            CashAction.Paid(ID);
            return RedirectToAction("ListApply");
        }

        public ActionResult ListPaid()
        {
            ViewBag.ListPaid = CashAction.ListPaid();
            return View();
        }

        public ActionResult OrderBill()
        {
            try
            {
                int a = (int)Session["UserID"];
                var listcash = BillAction.ListCartDetail(a);
                var detail = listcash.FirstOrDefault();
                CashAction.Order(a);
                return RedirectToAction("ViewCart","Cart");
               
            } catch {
                return RedirectToAction("ViewCart", "Cart");
            }
            
        }
        #endregion
    }
}