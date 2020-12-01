using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCuaHangSach.Models;
using System.Data.Entity;
namespace WebCuaHangSach.Action
{
    public class ContactInfoAction
    {
        public static Account InfoAccount(int ID)
        {
            Account a = null;
            using (var db = new BookContext())
            {
                a = db.Accounts.Include(x => x.Role).Where(m => m.ID == ID).FirstOrDefault();
            }
            return a;
        }
        public static List<ContactInfo> ListContact(int ID)
        {
            List<ContactInfo> a = null;
            using (var db = new BookContext())
            {
                a = db.ContactInfos.Where(m => m.AccountId == ID).ToList();
            }
            return a;
        }
        public static void AddInfoContact(int account_id, string AddressNumber, string Street, string District, string Province)
        {
            using (var db = new BookContext())
            {
                db.ContactInfos.Add(new ContactInfo {AccountId = account_id,AddressNumber = AddressNumber,Street = Street,
                District = District,Province = Province});
                db.SaveChanges();
                db.Dispose();
            }
        }
    }
}