using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebCuaHangSach.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string PublishingCompany { get; set; }
        public DateTime PublishingDate { get; set; }
        public string Size { get; set; }
        public int NumberOfPages { get; set; }
        public string CoverType { get; set; }
        [ForeignKey("BookType")]
        public int BookTypeID { get; set; }
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public int Discount { get; set; }
        public double ReducePrice { get; set; }
        public int Count { get; set; }

        [NotMapped]
        public List<Author> AuthorCollection { get; set; }

        [NotMapped]
        public List<BookType> BookTypeCollection { get; set; }

        public Author Author {get;set;}
        public BookType BookType { get; set; }
            
        public Book(string Name, string PublishingCompany, DateTime PublishingDate, 
            string Size, int NumberOfPages,  string CoverType, int BookTypeID, int AuthorID,
            string Image, double Price, int Discount)
        {
            this.Name = Name;
            this.PublishingCompany = PublishingCompany;
            this.PublishingDate = PublishingDate;
            this.Size = Size;
            this.NumberOfPages = NumberOfPages;
            this.CoverType = CoverType;
            this.BookTypeID = BookTypeID;
            this.AuthorID = AuthorID;
            this.Image = Image;
            this.Price = Price;
            Count = 0;
            this.Discount = Discount;
            this.ReducePrice =  (Price * Discount) / 100;
            IsDeleted = false;
        }

        public Book(Book another)
        {
            this.Name = another.Name;
            this.PublishingCompany = another.PublishingCompany;
            this.PublishingDate = another.PublishingDate;
            this.Size = another.Size;
            this.NumberOfPages = another.NumberOfPages;
            this.CoverType = another.CoverType;
            this.BookTypeID = another.BookTypeID;
            this.Image = another.Image;
            this.Price = another.Price;
            IsDeleted = false;
        }
        public Book()
        {

        }
    }
}