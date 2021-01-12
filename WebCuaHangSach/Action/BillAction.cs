using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebCuaHangSach.Models;

namespace WebCuaHangSach.Action
{
    public class BillAction
    {
        
        #region 'Add'
        //Add Cart with a Cart
        public static string AddCart(int AccountId,int BookId,int Count)
        {
            using (var db = new BookContext())
            {
                try
                {
                    var bill = db.Bills.Where(b => b.AccountID == AccountId && b.IsOrdered==false).FirstOrDefault();
                    if (bill == null)
                    {
                        bill = AddOnlyCart(AccountId, DateTime.Now);
                    }
                    BillAction.AddCartDetail(bill.ID, BookId, Count);
                    db.SaveChanges();
                    db.Dispose();
                    return "Add Cart successfully";
                }
                catch(Exception e)
                {
                    return e.Message;
                }
            }
        }
        public static string AddSingle(int AccountId, int BookId)
        {
            using (var db = new BookContext())
            {
                try
                {
                    var bill = db.Bills.Where(b => b.AccountID == AccountId && b.IsOrdered == false).FirstOrDefault();
                    if (bill == null)
                    {
                        bill = AddOnlyCart(AccountId, DateTime.Now);
                    }
                    BillAction.AddCartDetail(bill.ID, BookId,1);
                    db.SaveChanges();
                    db.Dispose();
                    return "Add Cart successfully";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
        }

        public static Bill AddOnlyCart(int AccountId,DateTime FoundedDate)
        {
            using (var db = new BookContext())
            {
                var bill = new Bill(AccountId, FoundedDate);
                db.Bills.Add(bill);
                db.SaveChanges();
                db.Dispose();
                return bill;
            }
        }
        //Add a Bill Detail
        public static void AddCartDetail(int BillID, int BookID, int Count)
        {
            using (var db = new BookContext())
            {
                var billdetail = db.BillDetails.Where(bd => (bd.BillID == BillID && bd.BookID == BookID &&bd.IsDeleted==false))
                    .FirstOrDefault();
                var book = db.Books.Find(BookID);
                if (billdetail == null )
                {
                    billdetail = new BillDetail(BillID, BookID, Count);
                    db.BillDetails.Add(billdetail);
                }
                else
                {
                    billdetail.Count += Count;
                }
                //caculate total price of Bill detail from book price
                double AddValue = book.ReducePrice * Count;
                billdetail.Price += AddValue;
                //caculate cost of Bill
                var bill = db.Bills.Find(BillID);
                bill.TotalCost += AddValue;
                //update database
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }
       
        //Add Cart to bill 
        public static void AddCartToBill(int AccountId)
        {
            using (var db = new BookContext())
            {
                var bill = db.Bills.Where(b=>b.AccountID==AccountId).FirstOrDefault();
                bill.IsPaid = true;
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }
        #endregion

        #region'Search'
        public static Bill FindBill(int ID)
        {
            using (var db = new BookContext())
            {
                var bill = db.Bills.Find(ID);
                if (bill != null)
                {
                    db.Dispose();
                    return bill;
                }
                db.Dispose();
                return null;
            }
        }

        
        public static List<BillDetail> ListCartDetail(int AccountId)
        {
            using (var db = new BookContext())
            {
                List<BillDetail> listdetail = null;
                var bill = db.Bills.Where(b => b.AccountID == AccountId && b.IsOrdered==false).FirstOrDefault();
                
                if (bill != null && bill.IsPaid==false)
                {
                    listdetail = db.BillDetails.Include(x =>x.Book)
                        .Include(x => x.Bill)
                        .Where(bd => (bd.BillID == bill.ID && bd.IsDeleted==false)).ToList();
                }
                db.Dispose();
                return listdetail;
            }
        }

        #endregion
        public static void UpdateCart(int BillDetailID, int Count)
        {
            using (var db = new BookContext())
            {
                var billDetail = db.BillDetails.Include(ct => ct.Book).Where(ct => ct.ID == BillDetailID).FirstOrDefault();
                var giohang = db.Bills.Where(gh => gh.ID == billDetail.BillID).FirstOrDefault();
                giohang.TotalCost -= billDetail.Count * billDetail.Book.ReducePrice;
                giohang.TotalCost += Count * billDetail.Book.ReducePrice;
                billDetail.Count = Count;
                billDetail.Price = Count * billDetail.Book.ReducePrice;
                db.Entry(billDetail).State = EntityState.Modified;
                db.Entry(giohang).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }
        #region'Delete'
        public static void DeleteCartDetail(int Id)
        {
            using (var db = new BookContext())
            {
                var billDetail = db.BillDetails.Find(Id);
                if (billDetail != null)
                {
                    billDetail.IsDeleted = true;
                    var bill = db.Bills.Find(billDetail.BillID);
                    bill.TotalCost -= billDetail.Price;
                    db.Entry(billDetail).State = EntityState.Modified;
                    db.Entry(bill).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Dispose();
            }
        }
        public static void DeleteCart(int AccountId)
        {
            using (var db = new BookContext())
            {
                var bill = db.Bills.Where(b => b.AccountID==AccountId).FirstOrDefault();
                if (bill != null && bill.IsPaid==false)
                {
                    bill.IsDeleted = true;
                    db.Entry(bill).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Dispose();
            }
        }
        public static void UnOrder(int AccountId)
        {
            using (var db = new BookContext())
            {
                var bill = db.Bills.Where(b => b.AccountID == AccountId).FirstOrDefault();
                if (bill != null && bill.IsPaid == true)
                {
                    bill.IsDeleted = true;
                    db.Entry(bill).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Dispose();
            }
        }
        #endregion
    }
}