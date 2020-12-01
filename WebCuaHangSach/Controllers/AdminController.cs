using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuaHangSach.Action;
using WebCuaHangSach.Models;

namespace WebCuaHangSach.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListAccount()
        {

            ViewBag.ListAccount = AccountAction.ListAccount();
            Account account = new Account();
            using (var db = new BookContext())
            {
                account.RoleCollection = db.Roles.ToList();
            }
            //AccountAction.
            return View(account);
        }
        [HttpPost]
        public ActionResult ListAccount(string SearchString)
        {
            ViewBag.ListAccount = AccountAction.Search(SearchString);
            return View();
        }

        [HttpGet]
        public ActionResult ListBook()
        {
            ViewBag.ListBook = BookAction.ListBook();
            //AccountAction.
            return View();
        }
        [HttpPost]
        public ActionResult ListBook(string SearchString)
        {
            ViewBag.ListBook = BookAction.SearchBook(SearchString);
            return View();
        }

        [HttpGet]
        public ActionResult ListLog()
        {

            ViewBag.ListLog = LogAction.ListLog();
           
            return View();
        }
        [HttpPost]
        public ActionResult ListLog(string SearchString)
        {
            ViewBag.ListLog = LogAction.Search(SearchString);
            //AccountAction.
            return View();
        }

        [HttpGet]
        public ActionResult ListAuthor()
        {

            ViewBag.ListAuthor = AuthorAction.ListAuthor();
            
            return View();
        }
        [HttpGet]
        public ActionResult ListBookType()
        {

            ViewBag.ListBookType = BookTypesAction.ListBookType();
            
            return View();
        }
    }
}