using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebCuaHangSach.Models
{
    public class BillDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [ForeignKey("Bill")]
        public int BillID { get; set; }
        [ForeignKey("Book")]
        public int BookID { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        //Delete property
        public bool IsDeleted { get; set; }
        //Cash property, false=cash , true= BillDetail

        public Bill Bill { get; set; }
        public Book Book { get; set; }
        public BillDetail(int BillID, int BookID, int Count)
        {
            this.BillID = BillID;
            this.BookID = BookID;
            this.Count = Count;
            Price = 0;  
            IsDeleted = false;
        }
        public BillDetail(BillDetail another)
        {
            this.ID = another.ID;
            this.BookID = another.BookID;
            this.Count = another.Count;
            this.Price = another.Price;
            IsDeleted = false;
        }
        public BillDetail()
        { }
        
    }
}