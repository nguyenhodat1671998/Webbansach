using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebCuaHangSach.Action;
using WebCuaHangSach.Models;

namespace WebCuaHangSach.Action
{
    public class BookAction
    {
        #region'Add'
        public static void AddBook(Book newBook)
        {
            try
            {
                if (newBook != null)
                {
                    using (var db = new BookContext())
                    {
                        db.Books.Add(new Book(newBook));
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
        
        public static void AddBook(string Name, string PublishingCompany, 
            DateTime PublishingDate,string Size, int NumberOfPages, string CoverType, 
            int BookTypeID, int AuthorID, string Image, double Price,int Discount)
        {
            using (var db = new BookContext())
            {
                db.Books.Add(new Book(Name, PublishingCompany,PublishingDate, Size, 
                    NumberOfPages,CoverType,BookTypeID, AuthorID, Image, Price,Discount));
                db.SaveChanges();
                db.Dispose();
            }
        }
        #endregion

        

        #region'Search'
        public static Book FindBook(int ID)
        {
            using (var db = new BookContext())
            {
                var sach = db.Books.Include(b => b.Author)
                    .Include(b => b.BookType)
                    .Where(b => b.ID == ID).FirstOrDefault();
                db.Dispose();
                return sach;
            }
        }
       
        
        public static List<Book> ListBook()
        {
            using (var db = new BookContext())
            {
                var list = db.Books.Include(x => x.BookType)
                    .Include(x => x.Author)
                    .Where(b => b.IsDeleted == false)
                    .OrderBy(b => b.Price).ToList();
                db.Dispose();
                return list;
            }
        }

        public static List<BookType> ListBookType()
        {
            using (var db = new BookContext())
            {
                var list = db.BookTypes.Where(b => b.IsDeleted == false).ToList();
                db.Dispose();
                return list;
            }
        }
        #endregion

        #region 'Modified'

        public static bool AddCount(int ID, int Count)
        {
            using (var db = new BookContext())
            {
                var sach = db.Books.Find(ID);
                if (sach != null)
                {
                    sach.Count += Count;
                    db.Entry(sach).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();
                    return true;
                    
                }
                return false;
            }
        }

        public static void EditFullBook(int ID, string Name, string PublishingCompany, 
            DateTime PublishingDate,string Size, int NumberOfPages, string CoverType, 
            int BookTypeID, int AuthorID, string Image, double Price,int Discount)
        {
            using (var db = new BookContext())
            {
                var sach = db.Books.Find(ID);
                if (sach != null)
                {
                    sach.Price = Price;
                    sach.Name = Name;
                    sach.PublishingCompany = PublishingCompany;
                    sach.PublishingDate = PublishingDate;
                    sach.Size = Size;
                    sach.NumberOfPages = NumberOfPages;
                    sach.CoverType = CoverType;
                    sach.BookTypeID = BookTypeID;
                    sach.AuthorID = AuthorID;
                    sach.Image = Image;
                    sach.Discount = Discount;
                    db.Entry(sach).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Dispose();
            }
        }

        public static void EditBook(int ID, string Name, double Price)
        {
            using (var db = new BookContext())
            {
                var sach = db.Books.Find(ID);
                if (sach != null)
                {
                    sach.Name = Name;
                    sach.Price = Price;
                    db.Entry(sach).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Dispose();
            }
        }
        #endregion

        #region'Delete'
        public static void DeleteBook(int ID)
        {
            using (var db = new BookContext())
            {
                var book = db.Books.Find(ID);
                if (book != null)
                {
                    book.IsDeleted = true;
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Dispose();
            }
        }
        #endregion

        #region PhanLoai


        public static List<Book> ListTruyen()
        {
            List<Book> a = null;
            using (var db = new BookContext())
            {
                a = db.Books.Include(x => x.Author).Where(m => m.IsDeleted == false && m.BookTypeID == 1).ToList();
            }
            return a;
        }

        public static List<Book> ListThieuNhi()
        {
            List<Book> a = null;
            using (var db = new BookContext())
            {
                a = db.Books.Include(x => x.Author).Where(m => m.IsDeleted == false && m.BookTypeID == 2).ToList();
            }
            return a;
        }

        public static List<Book> ListVanHoc()
        {
            List<Book> a = null;
            using (var db = new BookContext())
            {
                a = db.Books.Include(x => x.Author).Where(m => m.IsDeleted == false && m.BookTypeID == 3).ToList();
            }
            return a;
        }
        public static List<Book> ListTrinhTham()
        {
            List<Book> a = null;
            using (var db = new BookContext())
            {
                a = db.Books.Include(x => x.Author).Where(m => m.IsDeleted == false && m.BookTypeID == 4).ToList();
            }
            return a;
        }
        public static List<Book> SearchBook(string name)
        {
            List<Book> a = null;
            using (var db = new BookContext())
            {
                a = db.Books.Include(x => x.Author).Where(m => m.Name.Contains(name) && m.IsDeleted == false).ToList();
            }
            return a;
        }
        #endregion
    }


}