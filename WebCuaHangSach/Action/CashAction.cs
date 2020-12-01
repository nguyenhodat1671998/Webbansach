using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebCuaHangSach.Models;

namespace WebCuaHangSach.Action
{
    public class CashAction
    {
        public static void Order(int ID)
        {
            using (var db = new BookContext())
            {
                var a = db.Bills.Where(m => m.AccountID == ID && m.IsOrdered == false).FirstOrDefault();
                a.IsOrdered = true;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }
        #region CashManagement
        public static List<Bill> ListNotApply()
        {
            List<Bill> a = null;
            using (var db = new BookContext())
            {
                a = db.Bills.Include(x => x.Account).Where(m => m.IsApplied == false && m.IsOrdered == true &&
                m.IsDeleted == false).ToList();

            }
            return a;
        }

        public static void Apply(int ID)
        {
            using (var db = new BookContext())
            {
                var a = db.Bills.Where(m => m.ID == ID).FirstOrDefault();
                a.IsApplied = true;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }

        public static void RemoveBill(int ID)
        {
            using (var db = new BookContext())
            {
                var a = db.Bills.Where(m => m.ID == ID).FirstOrDefault();
                a.IsDeleted = true;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }

        public static List<Bill> ListApply()
        {
            List<Bill> a = null;
            using (var db = new BookContext())
            {
                a = db.Bills.Include(x => x.Account).Where(m => m.IsApplied == true && m.IsOrdered == true &&
               m.IsPaid == false && m.IsDeleted == false).ToList();

            }
            return a;
        }

        public static void Paid(int ID)
        {
            using (var db = new BookContext())
            {
                var a = db.Bills.Where(m => m.ID == ID).FirstOrDefault();
                a.IsPaid = true;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static List<Bill> ListPaid()
        {
            List<Bill> a = null;
            using (var db = new BookContext())
            {
                a = db.Bills.Include(x => x.Account).Where(m => m.IsApplied == true && m.IsOrdered == true &&
               m.IsPaid == true && m.IsDeleted == false).ToList();

            }
            return a;
        }
        #endregion
    }
}