using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebCuaHangSach.Models
{
    public class Account
    {
        //Account Info
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public DateTime LastLoginDate { get; set; }

        //Personal Infomation
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //Point
        public double Point { get; set; }
        //Delete property
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }


        [NotMapped]
        public string LoginErrorMessage { get; set; }
        [NotMapped]
        public List<Role> RoleCollection { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public Role Role { get; set; }

        //Constructor
        public Account(string UserName, string Password, string FirstName, string LastName,
            string PhoneNumber, string Email)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.RoleID = 2;
            this.Point = 0;
            this.LastLoginDate = DateTime.Now;
            IsDeleted = false;
            this.IsLocked = false;
        }
        public Account(Account another)
        {
            this.UserName = another.UserName;
            this.Password = another.Password;
            this.FirstName = another.FirstName;
            this.LastName = another.LastName;
            this.PhoneNumber = another.PhoneNumber;
            this.Email = another.Email;
            this.RoleID = another.RoleID;
            this.Point = another.Point;
            IsDeleted = false;
        }

        public Account()
        { }
    }
}