using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebCuaHangSach.Models;

namespace WebCuaHangSach.Action
{
    public class BookTypesAction
    {
        public static List<BookType> ListBookType()
        {
            List<BookType> a = null;
            using (var db = new BookContext())
            {
                a = db.BookTypes.Where(m => m.IsDeleted == false).ToList();
            }
            return a;
        }

        public static void AddBookType(string Name)
        {
            using (var db = new BookContext())
            {
                db.BookTypes.Add(new BookType{Name = Name, IsDeleted = false });
                db.SaveChanges();
                db.Dispose();
            }
        }

        public static BookType BookType(int ID)
        {
            BookType a = null;
            using (var db = new BookContext())
            {
                a = db.BookTypes.Where(m => m.ID == ID).FirstOrDefault();
            }
            return a;
        }

        public static void DeletedBookType(int ID)
        {
            using (var db = new BookContext())
            {
                var a = db.BookTypes.Find(ID);
                a.IsDeleted = true;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }

        public static void ModifyBookType(int ID,string Name)
        {
            using (var db = new BookContext())
            {
                var a = db.BookTypes.Find(ID);
                a.Name = Name;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }
    }
}