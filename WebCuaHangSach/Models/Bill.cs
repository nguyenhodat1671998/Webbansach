using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebCuaHangSach.Models
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("Account")]
        public int AccountID { get; set; }
        public DateTime FoundedDate { get; set; }
        public double TotalCost { get; set; }
        //Paid and Delete properties
        public bool IsPaid { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsOrdered { get; set; }
        public bool IsApplied { get; set; }

        public Account Account { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }

        public Bill(int AccountID, DateTime FoundedDate)
        {
            this.AccountID = AccountID;
            this.FoundedDate = FoundedDate;
            TotalCost = 0;
            this.IsDeleted = false;
            this.IsPaid = false;
            this.IsOrdered = false;
            this.IsApplied = false;
        }
        public Bill(Bill another)
        {
            this.AccountID = another.AccountID;
            this.FoundedDate = another.FoundedDate;
            this.TotalCost = another.TotalCost;
            this.IsDeleted = false;
            this.IsPaid = false;
            this.IsOrdered = false;
            this.IsApplied = false;
        }
        public Bill()
        {

        }
    }
}