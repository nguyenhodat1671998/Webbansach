using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebCuaHangSach.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }

        public Role()
        { }
    }
}