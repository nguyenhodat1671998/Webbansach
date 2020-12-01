using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuaHangSach.Action;
using WebCuaHangSach.Models;

namespace WebCuaHangSach.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult ViewCart()
        {
            if(Session["UserName"] != null)
            {
                int AccountId = (int)Session["UserID"];
                var listCart = BillAction.ListCartDetail(AccountId);

                if (listCart != null)
                {
                    ViewBag.ListCart = listCart;
                    ViewBag.Detail = listCart.FirstOrDefault();
                }
                return View();
            }
            return RedirectToAction("Index","Book");
        }

        public ActionResult AddSingle(int ID)
        {
            if (Session["UserName"] != null)
            {
                int AccountId = (int)Session["UserID"];
                BillAction.AddSingle(AccountId,ID);
                return RedirectToAction("ShopGrid", "Book");
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult AddCart(int BookId,int Count)
        {
            if (Session["UserName"] != null)
            {
                int AccountId = (int)Session["UserID"];
                ViewBag.Message = BillAction.AddCart(AccountId, BookId, Count);
                return RedirectToAction("Detail", "Book", new { ID = BookId });
            }
            return RedirectToAction("Login", "Account");
        }
        
        public ActionResult Order(int BillId)
        {
           
            BillAction.AddCartToBill(BillId);
            return RedirectToAction("ViewCart");
        }
        public ActionResult DeleteCart(int Id)
        {
            
            BillAction.DeleteCartDetail(Id);
            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public ActionResult UpdateCart(int ID, int Count)
        {
            BillAction.UpdateCart(ID, Count);
            return RedirectToAction("ViewCart");
        }
        [HttpGet]
        public ActionResult Checkout(int BillID)
        {
            if (Session["UserName"] != null)
            {
                int AccountId = (int)Session["UserID"];
                ViewBag.Bill = BillAction.FindBill(AccountId);
                return View();
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult ManageInfo()
        {
            if (Session["UserID"] == null)
            {
                //ViewBag.ListInfo = AccountAction.ListInfo();
                return View();
            }
            return RedirectToAction("Index", "Book");
            
        }
    }
}