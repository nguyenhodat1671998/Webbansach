using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuaHangSach.Action;

namespace WebCuaHangSach.Controllers
{
    public class BookTypeController : Controller
    {
        // GET: BookType
        

        public ActionResult DeletedBookType(int ID)
        {
            BookTypesAction.DeletedBookType(ID);
            return RedirectToAction("ListBookType", "Admin");
        }

        public ActionResult AddBookType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBookType(string Name)
        {
            BookTypesAction.AddBookType(Name);
            return RedirectToAction("AddBookType");
        }

        public ActionResult ModifyBookType(int ID)
        {
            ViewBag.BookType = BookTypesAction.BookType(ID);
            return View();
        }

        [HttpPost]
        public ActionResult ModifyBookType(int ID,string Name)
        {
            BookTypesAction.ModifyBookType(ID,Name);
            return RedirectToAction("ListBookType", "Admin");
        }
    }
}