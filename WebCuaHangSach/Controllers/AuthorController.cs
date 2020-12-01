using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuaHangSach.Action;

namespace WebCuaHangSach.Controllers
{
    public class AuthorController : Controller
    {
       

        public ActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAuthor(string Name,string Description)
        {
            AuthorAction.AddAuthor(Name,Description);
            return RedirectToAction("ListAuthor","Admin");
        }
        public ActionResult DeletedAuthor(int ID)
        {
            AuthorAction.DeletedAuthor(ID);
            return RedirectToAction("ListAuthor","Admin");
        }

        public ActionResult ModifyAuthor(int ID)
        {
            ViewBag.Author = AuthorAction.Author(ID);
            return View();
        }
        [HttpPost]
        public ActionResult ModifyAuthor(int ID,string Name,string Description)
        {
            AuthorAction.ModifyAuthor(ID,Name,Description);
            return RedirectToAction("ListAuthor","Admin");
        }
    }
}