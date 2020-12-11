using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebCuaHangSach.Models;

namespace WebCuaHangSach.Action
{
    public class AccountAction
    {
        #region 'Add'
       
        public static void AddAccount(Account newAccount)
        {
            using (var db = new BookContext())
            {
                var account = new Account(newAccount);
                db.Accounts.Add(account);
                db.SaveChanges();
            }
        }

        public static void AddAccount(string UserName, string PassWord, 
            string FirstName, string LastName, string PhoneNumber, string Email)
        {
            using (var db = new BookContext())
            {
                var account = new Account(UserName, PassWord, FirstName,
                    LastName, PhoneNumber, Email);
                db.Accounts.Add(account);
                db.SaveChanges();
                db.Dispose();
            }
        }
        public static bool Add(string FirstName, string LastName, string Email, string PhoneNumber)
        {
            using (var db = new BookContext())
            {
                var account = new Account() { FirstName=FirstName,LastName=LastName,
                    Email =Email,PhoneNumber=PhoneNumber,IsDeleted=false,Point=0
                    ,LastLoginDate=DateTime.Now, RoleID=2};
                db.Accounts.Add(account);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
        }

        public static Account VerifyAccount(string UserName,string Password)
        {
            using (var db = new BookContext())
            {
                Account account = db.Accounts.Where(acc => acc.UserName.Equals(UserName) && acc.Password==Password && acc.IsLocked==false ).FirstOrDefault();
                if(account !=null && account.IsDeleted==false)
                {
                    return account;
                }
                return null;
            }
        }

        #endregion


        #region'Search'
        public static Account FindAccount(int ID)
        {
            using (var db = new BookContext())
            {
                var acc = db.Accounts.Find(ID);
                if (acc != null)
                {
                    db.Dispose();
                    return acc;
                }
                db.Dispose();
                return null;
            }
        }
        public static Account FindAccount(string UserName)
        {
            using (var db = new BookContext())
            {
                var acc = db.Accounts.Where(a => a.UserName.Equals(UserName)).Single();
                if (acc != null)
                {
                    db.Dispose();
                    return acc;
                }
                db.Dispose();
                return null;
            }
        }
        public static List<Account> FindAccountWithRole(string Role)
        {
            using (var db = new BookContext())
            {
                var listacc = db.Accounts
                    .Where(a => a.Role.Description.Equals(Role))
                    .OrderBy(a => a.Point).ToList();
                if (listacc != null)
                {
                    db.Dispose();
                    return listacc;
                }
                db.Dispose();
                return null;
            }
        }
        public static List<Account> FindAccount(double LowerPoint, double UpperPoint)
        {
            using (var db = new BookContext())
            {
                var list = db.Accounts
                    .Where(b => b.Point >= LowerPoint && b.Point <= UpperPoint)
                    .OrderBy(b => b.Point).ToList();
                db.Dispose();
                return list;
            }
        }

        public static List<Account> ListAccount()
        {
            using (var db = new BookContext())
            {
                var list = db.Accounts.Include(acc =>acc.Role)
                    .Where(acc => acc.IsDeleted == false).ToList();
                db.Dispose();
                return list;
            }
        }

        public static List<Account> Search(string SearchString)
        {
            using (var db = new BookContext())
            {
                var list = db.Accounts.Include(acc => acc.Role)
                    .Where(acc => acc.IsDeleted == false && (acc.FirstName.Contains(SearchString) || acc.LastName.Contains(SearchString)|| acc.PhoneNumber.Contains(SearchString) )).ToList();
                db.Dispose();
                return list;
            }
        }

        
        #endregion


        #region 'Modified'
        

        public static bool EditRole(int ID, int Role_id)
        {
            using (var db = new BookContext())
            {
                var acc = db.Accounts.Find(ID);
                acc.RoleID = Role_id;
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
                return true;
            }
        }
        public static bool Edit(int ID,string FirstName, string LastName,string Email, string PhoneNumber)
        {
            using (var db = new BookContext())
            {
                var acc = db.Accounts.Find(ID);
                acc.FirstName = FirstName;
                acc.LastName = LastName;
                acc.PhoneNumber = PhoneNumber;
                acc.Email = Email;
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
                return true;
            }
        }
        public static bool ChangePassword(string UserName , string Password, string NewPassword )
        {
            using (var db = new BookContext())
            {
                var account = db.Accounts.Where(acc => acc.UserName.Equals(UserName)).Single();
                if (account != null)
                    if (account.Password.Equals(Password))
                    {
                        account.Password = NewPassword;
                        db.Entry(account).State = EntityState.Modified;
                        db.SaveChanges(); db.Dispose();
                        return true;
                    }
                db.Dispose();
                return false;
            }
        }

        public static bool Lock(int ID)
        {
            using (var db = new BookContext())
            {
                var acc = db.Accounts.Find(ID);
                acc.IsLocked = true ;
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
                return true;
            }
        }

        public static bool UnLock(int ID)
        {
            using (var db = new BookContext())
            {
                var acc = db.Accounts.Find(ID);
                acc.IsLocked = false;
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
                return true;
            }
        }

        #endregion


        #region'Delete'
        public static void DeleteAccount(int ID)
        {
            using (var db = new BookContext())
            {
                var acc = db.Accounts.Find(ID);
                if (acc != null)
                {
                    acc.IsDeleted = true;
                    db.Entry(acc).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Dispose();
            }
        }
        #endregion

    }
}
