using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebCuaHangSach.Models
{
    public class AccountLogin
    {
        [Required(ErrorMessage = "Vui lòng nhập UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Password")]
        public string Password { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public DateTime LastLoginDate { get; set; }

        //Personal Infomation
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
      
    }
}