using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCuaHangSach.Models;
using System.Data.Entity;
namespace WebCuaHangSach.Action
{
    public class LogAction
    {
        public static List<Log> ListLog()
        {
            using (var db = new BookContext())
            {
                var list = db.Logs.Include(x => x.Account).ToList();
                db.Dispose();
                return list;
            }
        }
        public static List<Log> Search(string SearchString)
        {
            using (var db = new BookContext())
            {
                var list = db.Logs.Include(x => x.Account)
                    .Where(lg => lg.Activity.Contains(SearchString) || lg.Account.UserName.Contains(SearchString) 
                    || lg.Account.ID==int.Parse(SearchString)).ToList();//can than khong the chuyen string sang int la co bug
                db.Dispose();
                return list;
            }
        }
        public static void WriteLog(int Account_id, string Activity)
        {
           

            using (var db = new BookContext())
            {
                db.Logs.Add(new Log(Account_id, Activity));
            }
        }
       
    }
}