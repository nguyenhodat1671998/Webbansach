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

        public string OrderBill()
        {
            int a = (int)Session["UserID"];
            var listcash = BillAction.ListCartDetail(a);
            var detail = listcash.FirstOrDefault();
            string text = "";
            foreach (var item in listcash)
            {
                text += item.Book.Name + "<br/>" + item.Price + "\n" + item.Count;
            }
            CashAction.Order(a);
            string content = System.IO.File.ReadAllText(Server.MapPath("~/client/template/neworder.html"));

            content = content.Replace("{{Name}}", "sadsada");
            content = content.Replace("{{product}}", text);
            content = content.Replace("{{Content}}", "allday997@gmail.com");
            content = content.Replace("{{totalcost}}", Convert.ToString(detail.Bill.TotalCost));
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail("allday997@gmail.com", "Đơn Đặt Hàng", content);
            new MailHelper().SendMail(toEmail, "Đơn Đặt Hàng", content);
            return "ok";
        }
        #endregion
    }
}