using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebCuaHangSach.Models;

namespace WebCuaHangSach.Action
{
    public class AuthorAction
    {
        public static List<Author> ListAuthor()
        {
            List<Author> a = null;
            using (var db = new BookContext())
            {
                a = db.Authors.Where(m => m.IsDeleted == false).ToList();
            }
            return a;
        }

        public static void AddAuthor(string Name, string Description)
        {
            using (var db = new BookContext())
            {
                db.Authors.Add(new Author { Name = Name, Description = Description, IsDeleted = false });
                db.SaveChanges();
                db.Dispose();
            }
        }

        public static void DeletedAuthor(int ID)
        {
            using (var db = new BookContext())
            {
                var a = db.Authors.Find(ID);
                a.IsDeleted = true;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }

        public static Author Author(int ID)
        {
           Author a = null;
            using (var db = new BookContext())
            {
                a = db.Authors.Where(m => m.ID == ID).FirstOrDefault();
            }
            return a;
        }

        public static void ModifyAuthor(int ID,string Name,string Description)
        {
            using (var db = new BookContext())
            {
                var a = db.Authors.Find(ID);
                a.Name = Name;
                a.Description = Description;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }
    }
}