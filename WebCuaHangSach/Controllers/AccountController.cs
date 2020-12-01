using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuaHangSach.Action;
using WebCuaHangSach.Models;

namespace WebCuaHangSach.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (Session["Account_UserName"] == null)
            {
                Session["Account_Role"] = 0;
            }
            return View();
        }



        #region 'Add'
        [HttpGet]
        public ActionResult Register()
        {
            if (Session["Account_UserName"] == null)
            {
                Session["Account_Role"] = 0;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Register(string UserName, string PassWord,string RePassWord,
            string FirstName, string LastName, string PhoneNumber, string Email)
        {
            if (Session["Account_UserName"] == null)
            {
                Session["Account_Role"] = 0;
            }
            if (PassWord.Equals(RePassWord))
            {
                AccountAction.AddAccount(UserName, PassWord, FirstName, LastName, PhoneNumber, Email);
                ViewBag.Message = "Register successfully";
            }
            
            return View();
        }

        public ActionResult Add()
        {
            
            ViewBag.ListAccount = AccountAction.ListAccount();
            //AccountAction.
            return View();
        }
        [HttpPost]
        public ActionResult Add(string FirstName, string LastName, string Email, string PhoneNumber)
        {
            try
            {
                ViewBag.ListAccount = AccountAction.ListAccount();
                AccountAction.Add(FirstName, LastName, Email, PhoneNumber);
                return RedirectToAction("Add");
            }
            catch
            {
                ViewBag.Message = "Thêm thất bại";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountLogin acc)
        {
            Account account = AccountAction.VerifyAccount(acc.UserName, acc.Password);
            if (account!=null)
            {
                Session["UserName"] = account.UserName;
                Session["Role"] = account.RoleID;
                Session["UserID"] = account.ID;
                if((int)Session["Role"]==1)
                {
                    return RedirectToAction("ListBook", "Admin");
                }
                return RedirectToAction("ShopGrid", "Book");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        #endregion
       

        public ActionResult DeleteAccount(int ID)
        {
            if (Session["Account_UserName"] == null)
            {
                Session["Account_Role"] = 0;
            }
            AccountAction.DeleteAccount(ID);
            return RedirectToAction("AccountManagement");
        }

        #region 'Modify'
        
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["Account_UserName"] == null)
            {
                Session["Account_Role"] = 0;
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangePasword(string UserName, string Password, string NewPassword)
        {
            
            bool IsVerified = AccountAction.ChangePassword(UserName, Password, NewPassword);
            if (IsVerified)
            {
                ViewBag.Message = "Change password successfully";
            }
            ViewBag.Message = "Wrong current password";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            ViewBag.Account = AccountAction.FindAccount(ID);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int ID, string Email, string PhoneNumber, string FirstName, string LastName )
        {
            
            Account acc = AccountAction.FindAccount(ID);
            ViewBag.Message = "Nhập thông tin cần cập nhật";
            if (acc.Email !=Email || acc.PhoneNumber!=PhoneNumber || acc.FirstName!=FirstName || acc.LastName!=LastName)
            {
                if (AccountAction.Edit(ID, FirstName, LastName, Email, PhoneNumber))
                {
                    ViewBag.Message = "Cập nhật thành công";
                }
            }
            ViewBag.Account = AccountAction.FindAccount(ID);
            return View();

        }

        [HttpGet]
        public ActionResult Lock(int ID)
        {
            AccountAction.Lock(ID);
            return RedirectToAction("ListAccount","Admin");
        }
        [HttpGet]
        public ActionResult UnLock(int ID)
        {
            AccountAction.UnLock(ID);
            return RedirectToAction("ListAccount", "Admin");
        }
        public ActionResult LogOut()
        {
            
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}